import React, { Component } from 'react';
import FollowingsComponent from './FollowingsComponent';
import axios from 'axios';

export class Followings extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: [],
            username: ""
        }

    }

    async componentDidMount() {
        await this.handleGetFollowings();
    }

    handleGetFollowings = async () => {
        let username = this.props.match.params.username;

        const result = await axios.get(`/api/Profiles/Followings?username=${username}`);

        this.setState({ data: result.data, username: username });
    }

    render() {
        return (
            <FollowingsComponent data={this.state.data} username={this.state.username} />
        );
    }
}