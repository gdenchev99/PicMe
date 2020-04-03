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

        this.handleChange = this.handleChange.bind(this);
        this.sendMessage = this.sendMessage.bind(this);
    }

    componentDidMount = async () => {
        await this.getMessages();

        const user = await authService.getUser();
        const myUsername = user.name;

        this.setState({myUsername: myUsername});

        // this.connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
        
        // this.connection.start()
        //     .then(() => console.log("Connection established!"))
        //     .catch(() => console.log("Connection failed."));


    }

    handleChange = ({ target }) => {
        this.setState({ [target.name]: target.value });
    };

    sendMessage = async () => {
        let user = await authService.getUser();
        let userId = user.sub;
        let userTwoUsername = this.props.match.params.username;

        let data = {
            userOneId: userId,
            userTwoUsername: userTwoUsername,
            text: this.state.text
        };

        axios.post("api/Messages/Send", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(r => console.log(r))
        .catch(e => console.log(e));
        
    }

    getMessages = async () => {
        let username = this.props.match.params.username;

        let result = await axios.get(`api/Messages/ChatRoom?username=${username}`);

        let receiverPicture = result.data.find(r => r.userUserName == username).userProfilePictureUrl;

        this.setState({data: result.data, receiverUsername: username, receiverPicture: receiverPicture});
    }

    render() {
        return (
            <PrivateMessageComponent data={this.state.data}
            state={this.state}
            sendMessage={this.sendMessage}
            handleChange={this.handleChange}/>
        );
    }
}