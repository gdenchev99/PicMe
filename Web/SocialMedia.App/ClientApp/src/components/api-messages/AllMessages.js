import React, { Component } from 'react';
import AllMessagesComponent from './AllMessagesComponent';
import authService from '../api-authorization/AuthorizeService';
import messagesService from './MessagesService';

export class AllMessages extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: []
        }
    }

    async componentDidMount() {
        await this.getChatRooms();
    }

    getChatRooms = async () => {
        let user = await authService.getUser();
        let userId = user.sub;

        let result = await messagesService.getChatRooms(userId);

        this.setState({data: result.data});
    }

    render() {
        return(
            <AllMessagesComponent data={this.state.data}/>
        );
    }
}