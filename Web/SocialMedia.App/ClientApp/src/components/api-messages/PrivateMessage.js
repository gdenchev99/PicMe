import React, { Component } from 'react';
import * as signalR from '@aspnet/signalr';
import authService from '../api-authorization/AuthorizeService';
import PrivateMessageComponent from './PrivateMessageComponent';

export class PrivateMessage extends Component {
    constructor(props) {
        super(props);
        
        this.state = {
            username: "",
            isAuthenticated: false,
            message: ""
        };

        this.connection = null;
    }

    componentDidMount = async () => {
        const user = await authService.getUser();
        const username = user.name;

        this.connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
        
        this.connection.start()
            .then(() => console.log("Connection established!"))
            .catch(() => console.log("Connection failed."));
    }

    render() {
        return (
            <PrivateMessageComponent />
        );
    }
}