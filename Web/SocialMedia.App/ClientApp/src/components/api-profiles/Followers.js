import React, { Component } from 'react';
import FollowersComponent from './FollowersComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';
import profileService from './ProfileService';
import { ButtonType } from './ProfileConstants';

export class Followers extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: [],
            username: ""
        }

    }

    async componentDidMount() {
        await this.handleGetFollowers();
    }

    handleGetFollowers = async () => {
        let username = this.props.match.params.username;

        const result = await axios.get(`/api/Profiles/Followers?username=${username}`);

        this.setState({ data: result.data, username: username });
    }

    render() {
        return (
            <FollowersComponent data={this.state.data} username={this.state.username}/>
        );
    }
}