import React, { Component } from 'react';
import FollowersComponent from './FollowersComponent';
import axios from 'axios';

export class Followers extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: []
        }

    }

    async componentDidMount() {
        await this.handleGetFollowers();
    }

    handleGetFollowers = async () => {
        let username = this.props.match.params.username;

        const result = await axios.get(`/api/Profiles/Followers?username=${username}`);

        this.setState({ data: result.data});
    }

    render() {
        return (
            <FollowersComponent data={this.state.data} />
        );
    }
}