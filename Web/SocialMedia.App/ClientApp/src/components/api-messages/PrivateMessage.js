import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';
import authService from '../api-authorization/AuthorizeService';
import PrivateMessageComponent from './PrivateMessageComponent';
import axios from 'axios';

export class PrivateMessage extends Component {
    constructor(props) {
        super(props);
        
        this.state = {
            myUsername: "",
            receiverUsername: "",
            receiverPicture: "",
            isAuthenticated: false,
            text: "",
            data: []
        };

        this.connection = null;
        this.defaultPicture = "https://res.cloudinary.com/dibntzvzk/image/upload/v1585424980/DefaultPhotos/240_F_64676383_LdbmhiNM6Ypzb3FM4PPuFP9rHe7ri8Ju_vcoocw.jpg";

        this.windowRef = React.createRef();

        this.handleChange = this.handleChange.bind(this);
        this.sendMessage = this.sendMessage.bind(this);
    }

    componentDidMount = async () => {
        await this.getMessages();

        /* Scroll to the bottom of the chat */
        this.windowRef.current.scrollTop = this.windowRef.current.scrollHeight;

        this.connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
        
        await this.connection.start()
            .then(() => console.log("Connection established!"))
            .catch(() => console.log("Connection failed."));

        await this.handleJoinChatRoom();
        this.handleAppendMessage();
    }

    handleChange = ({ target }) => {
        this.setState({ [target.name]: target.value });
    };

    handleJoinChatRoom = async () => {
        if (this.state.data.length > 0) {
            var roomId = this.state.data[0].chatRoomId;
            this.connection.invoke("JoinChatRoom", roomId)
        }
    }

    handleAppendMessage = () => {
        this.connection.on("ReceiveMessage", message => {
            let msgs = this.state.data;
            msgs.push(message);
            this.setState({text: "", data:msgs});
            this.windowRef.current.scrollTop = this.windowRef.current.scrollHeight;
        })
    }

    sendMessage = async () => {
        let user = await authService.getUser();
        let userId = user.sub;
        let userTwoUsername = this.props.match.params.username;

        let data = {
            userOneId: userId,
            userTwoUsername: userTwoUsername,
            text: this.state.text
        };

        this.connection.invoke("SendMessage", data);
    }

    getMessages = async () => {
        let receiverUsername = this.props.match.params.username;
        let user = await authService.getUser();
        let currentId = user.sub;

        let result = await axios.get(`api/Messages/ChatRoom?currentId=${currentId}&receiverUsername=${receiverUsername}`);

        let receiver = result.data.find(r => r.userUserName == receiverUsername);

        let receiverPicture = receiver == null ? this.defaultPicture : receiver.userProfilePictureUrl;

        this.setState({data: result.data, 
            receiverUsername: receiverUsername, 
            receiverPicture: receiverPicture,
            myUsername: user.name});
    }

    render() {
        return (
            <PrivateMessageComponent data={this.state.data}
            state={this.state}
            sendMessage={this.sendMessage}
            handleChange={this.handleChange}
            windowRef={this.windowRef}/>
        );
    }
}