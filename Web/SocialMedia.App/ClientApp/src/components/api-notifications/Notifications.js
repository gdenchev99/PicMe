import React, { Component } from 'react';
import authService from '../api-authorization/AuthorizeService';
import notificationsService from './NotificationsService';
import NotificationsComponent from './NotificationsComponent';

export class Notifications extends Component {
    constructor(props) {
        super(props);

        this.state = {
            currentUser: null,
            username: "",
            data: [],
            skipCount: 0,
            takeCount: 10
        }

        this.usernameRegex = /([a-zA-z0-9_]+)+?/;

        this.loadMore = this.loadMore.bind(this);
    }

    async componentDidMount() {
        let currentUser = await authService.getUser();
        let username = currentUser.name;
        this.setState({currentUser: currentUser, username: username});
        
        await this.handleData();
        await notificationsService.setStatusRead(this.state.currentUser.sub);
    }

    handleData = async () => {
        let userId = this.state.currentUser.sub;
        let skipCount = this.state.skipCount;
        let takeCount = this.state.takeCount;

        let data = await notificationsService.getNotifications(userId, skipCount, takeCount);
        this.setState({data: data, skipCount: this.state.skipCount + this.state.takeCount});
        console.log(data);
    }

    loadMore = async () => {
        let userId = this.state.currentUser.sub;
        let skipCount = this.state.skipCount;
        let takeCount = this.state.takeCount;

        let data = await notificationsService.getNotifications(userId, skipCount, takeCount);
        this.setState({data: this.state.data.concat(data), skipCount: this.state.skipCount + this.state.takeCount});
    }

    render() {
        return(
            <NotificationsComponent data={this.state.data} 
            username={this.state.username}
            regex={this.usernameRegex} 
            loadMore={this.loadMore}/>
        );
    }
}