import React, { Component, Fragment } from 'react';
import PostComponent from './PostComponent';
import axios from 'axios';

export class Post extends Component {

    constructor(props) {
        super(props);
        this.state = {
            data: {},
            isLoading: true
        }
    }

    async componentDidMount() {
        await this.handleData();
    }

    handleData = async() => {
        let path = this.props.location.pathname;
        let postId = path.substr(path.lastIndexOf('/') + 1);

        let response = await axios.get(`/api/Posts/Get?id=${postId}`)

        console.log(response.data);
        
        this.setState({data: response.data});

        this.setState({isLoading: false})
    }

    render() {

        return(
            <React.Fragment>
            {this.state.isLoading && 
                <div>Loading... </div>}
            {!this.state.isLoading &&
                <PostComponent data={this.state.data}/> }
            </React.Fragment>
        );
    }
}