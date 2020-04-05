import React, { Component } from 'react';
import AllMessagesComponent from './AllMessagesComponent';
import authService from '../api-authorization/AuthorizeService';
import axios from 'axios';

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

        let result = await axios.get(`/api/Messages/ChatRooms?userId=${userId}`);

        this.setState({data: result.data});
    }

    render() {
        return(
            <AllMessagesComponent data={this.state.data}/>
        );
    }
}