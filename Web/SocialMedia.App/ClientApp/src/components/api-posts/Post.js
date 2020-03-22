import React, { Component, Fragment } from 'react';
import PostComponent from './PostComponent';
import axios from 'axios';

export class Post extends Component {

    constructor(props) {
        super(props);
        this.state = {
            data: {},
            description: "",
            isLoading: true
        }

        var path = this.props.location.pathname;
        this.postId = path.substr(path.lastIndexOf('/') + 1);
    }

    async componentDidMount() {
        await this.handleData();
    }

    handleData = async() => {
        
        let response = await axios.get(`/api/Posts/Get?id=${this.postId}`)
        
        this.setState({ data: response.data, description: response.data.description });
        
        if (this.state.description == null) {
            this.setState({ description: "" })

        }

        this.setState({isLoading: false})
    }

    render() {

        return(
            <React.Fragment>
            {this.state.isLoading && 
                <div>Loading... </div>}
            {!this.state.isLoading &&
                <PostComponent state={this.state} data={this.state.data} postId={this.postId}/> }
            </React.Fragment>
        );
    }
}