import React, { Component } from 'react';
import FeedComponent from './FeedComponent';
import authService from '../api-authorization/AuthorizeService';
import postsService from './PostsService';

export class Feed extends Component {

    constructor(params) {
        super(params);
        this.state = {
            isAuthenticated: false,
            userId: "",
            data: [],
            skipCount: 0,
            takeCount: 2
        }

        this.loadMore = this.loadMore.bind(this);
    }

    async componentDidMount() {
        await this.populateState();
        await this.handleData();

    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userId: user && user.sub
        });
    }

    handleData = async () => {
        let response = await postsService.fetchFeedPosts(this.state.userId, this.state.skipCount, this.state.takeCount);

        this.setState({ data: response.data, skipCount: this.state.skipCount + this.state.takeCount });
    }

    loadMore = async () => {
        let response = await postsService.fetchFeedPosts(this.state.userId, this.state.skipCount, this.state.takeCount);

        this.setState({ data: this.state.data.concat(response.data), skipCount: this.state.skipCount + this.state.takeCount });
    }

    render() {
        return (
            <React.Fragment>
                    <FeedComponent data={this.state.data} loadMore={this.loadMore} />
            </React.Fragment>
        );
    }
}