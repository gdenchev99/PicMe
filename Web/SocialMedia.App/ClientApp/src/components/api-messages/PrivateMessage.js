import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';
import authService from '../api-authorization/AuthorizeService';
import PrivateMessageComponent from './PrivateMessageComponent';
import messagesService from './MessagesService';

export class PrivateMessage extends Component {
    constructor(props) {
        super(props);
        
        this.state = {
            currentUser: null,
            myUsername: "",
            currentId: "",
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
        this.handleAddEmoji = this.handleAddEmoji.bind(this);
        this.handleEmojiPicker = this.handleEmojiPicker.bind(this);
        this.handleCloseOnClick = this.handleCloseOnClick.bind(this);
    }

    componentDidMount = async () => {
        let user = await authService.getUser();
        let currentId = user.sub;

        this.setState({currentUser: user, currentId: currentId});

        await this.getMessages();

        /* Scroll to the bottom of the chat */
        this.windowRef.current.scrollTop = this.windowRef.current.scrollHeight;

        // Close emoji picker on click
        document.addEventListener("mousedown", this.handleCloseOnClick);

        this.connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").configureLogging(signalR.LogLevel.None).build();
        
        await this.connection.start()
            .then()
            .catch(() => console.log("Connection failed."));

        await this.handleJoinChatRoom();
        this.handleAppendMessage();
    }

    handleAddEmoji = e => {
        let emoji = e.native;
        this.setState({
            text: this.state.text + emoji
        });
    };

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

    sendMessage = () => {
        let userTwoUsername = this.props.match.params.username;

        let data = {
            userOneId: this.state.currentId,
            userTwoUsername: userTwoUsername,
            text: this.state.text
        };

        this.connection.invoke("SendMessage", data);
    }

    getMessages = async () => {
        let receiverUsername = this.props.match.params.username;

        let result = await messagesService.getMessages(this.state.currentId, receiverUsername);
        let receiver = result.data.find(r => r.userUserName == receiverUsername);

        let receiverPicture = receiver == null ? this.defaultPicture : receiver.userProfilePictureUrl;

        this.setState({data: result.data, 
            receiverUsername: receiverUsername, 
            receiverPicture: receiverPicture,
            myUsername: this.state.currentUser.name});
    }

    handleEmojiPicker = (event) => {
        let picker = document.getElementsByClassName("emoji-picker")[0];
        if (picker.style.display == "block") {
            picker.style.display = "none";
        } else {
            picker.style.display = "block";
        }
    }

     // Close the emoji box when you click outside of it.
     handleCloseOnClick = (event) => {
        try {
            let node = document.getElementsByClassName("emoji-picker")[0];
            if (!node.contains(event.target)) {
                // Handle outside click here
                node.style.display = "none";
            }
        } catch (error) {
            return null;
        }
    }

    render() {
        return (
            <PrivateMessageComponent data={this.state.data}
            state={this.state}
            showPicker={this.handleEmojiPicker}
            addEmoji={this.handleAddEmoji}
            sendMessage={this.sendMessage}
            handleChange={this.handleChange}
            windowRef={this.windowRef}/>
        );
    }
}