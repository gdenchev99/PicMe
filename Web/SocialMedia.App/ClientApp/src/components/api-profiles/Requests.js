import React, { Component } from 'react';
import RequestComponent from './RequetsComponent';
import authService from '../api-authorization/AuthorizeService';
import profileService from './ProfileService';

export class Requests extends Component {
    constructor(props) {
        super(props);

        this.state = {
            data: [],
            user: null
        }

        this.handleApprove = this.handleApprove.bind(this);
        this.handleDelete = this.handleDelete.bind(this);
    }

    async componentDidMount() {
        let user = await authService.getUser();
        let currentUsername = user.name;

        this.setState({ user: user });

        let username = this.props.match.params.username;

        if (currentUsername !== username) {
            return this.props.history.push('/404');
        }


        await this.handleData();
    }

    handleData = async () => {
        let id = this.state.user.sub;

        let result = await profileService.getRequests(id);

        this.setState({ data: result.data });
    }

    handleApprove = async (username, ev) => {
        ev.persist();
        let element = ev.target.parentNode.parentNode;

        await profileService.approveRequest(username)
            .then(() => {
                element.remove();
            })
            .catch(e => console.log(e));
    }

    handleDelete = async (username, ev) => {
        ev.persist();
        let element = ev.target.parentNode.parentNode;

        await profileService.deleteRequest(username)
            .then(() => {
                element.remove();
            })
            .catch(e => console.log(e));
    }

    render() {
        return (
            <RequestComponent data={this.state.data}
                handleApprove={this.handleApprove}
                handleDelete={this.handleDelete} />
        );
    }
}