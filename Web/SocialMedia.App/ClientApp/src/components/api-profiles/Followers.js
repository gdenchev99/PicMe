import React, { Component } from 'react';
import FollowersComponent from './FollowersComponent';
import profileService from './ProfileService';

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

        const result = await profileService.getFollowers(username);

        this.setState({ data: result.data});
    }

    render() {
        return (
            <FollowersComponent data={this.state.data} />
        );
    }
}