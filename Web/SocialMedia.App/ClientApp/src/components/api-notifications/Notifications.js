import React, { Component } from 'react';
import authService from '../api-authorization/AuthorizeService';
import notificationsService from './NotificationsService';
import NotificationsComponent from './NotificationsComponent';

export class Notifications extends Component {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: null,
            data: []
        }

        this.usernameRegex = /([a-zA-z0-9_]+)+?/;
    }

    async componentDidMount() {
        let currentUser = await authService.getUser();
        this.setState({currentUser: currentUser});
        
        await this.handleData();
        await notificationsService.setStatusRead(this.state.currentUser.sub);
    }

    handleData = async () => {
        let data = await notificationsService.getNotifications(this.state.currentUser.sub);
        this.setState({data: data});
    }

    render() {
        return(
            <NotificationsComponent data={this.state.data} regex={this.usernameRegex}/>
        );
    }
}