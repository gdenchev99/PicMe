import React, { Component } from 'react';
import FeedComponent from './FeedComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';

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

        let response = await axios.get(`/api/Posts/Feed?id=${this.state.userId}
        &skipCount=${this.state.skipCount}
        &takeCount=${this.state.takeCount}`);

        this.setState({ data: response.data, skipCount: this.state.skipCount + this.state.takeCount });
    }

    loadMore = async () => {
        let response = await axios.get(`/api/Posts/Feed?id=${this.state.userId}
        &skipCount=${this.state.skipCount}
        &takeCount=${this.state.takeCount}`);

        this.setState({ data: this.state.data.concat(response.data), skipCount: this.state.skipCount + this.state.takeCount });
    }

    render() {
        return (
            <React.Fragment>
                {this.state.isAuthenticated ?
                    <FeedComponent data={this.state.data} loadMore={this.loadMore} /> :
                    <div><center><h1>Hello! Please login in order to proceed!</h1></center></div>}
            </React.Fragment>
        );
    }
}