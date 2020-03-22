import React, { Component } from 'react';
import FeedComponent from './FeedComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';
import { Login } from '../api-authorization/Login';
import { LoginActions } from '../api-authorization/ApiAuthorizationConstants';

export class Feed extends Component {

    constructor(params) {
        super(params);
        this.state = {
            isAuthenticated: false,
            userId: "",
            data: []
        }
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

        let response = await axios.get(`/api/Posts/All?id=${this.state.userId}`)

        this.setState({ data: response.data });

        console.log(response.data);
    }

    render() {
        return (
            <React.Fragment>
                {this.state.isAuthenticated ?
                    <FeedComponent data={this.state.data} /> :
                    <div><center><h1>Hello! Please login in order to proceed!</h1></center></div>}
            </React.Fragment>
        );
    }
}