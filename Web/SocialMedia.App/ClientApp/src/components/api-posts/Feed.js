import React, { Component } from 'react';
import FeedComponent from './FeedComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';

export class Feed extends Component {

    constructor(params) {
        super(params);
        this.state = {
            data: []
        }
    }
    
    async componentDidMount() {
        await this.handleData();
    }

    handleData = async() => {

        let user = await authService.getUser();

        console.log(user);
        

        let userId = user.sub;

        let response = await axios.get(`/api/Posts/All?id=${userId}`)

        this.setState({data: response.data});
        
    }

    render() {
        return(
            <FeedComponent data={this.state.data} />
        );
    }
}