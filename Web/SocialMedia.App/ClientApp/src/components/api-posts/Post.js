import React, { Component, Fragment } from 'react';
import PostComponent from './PostComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';

export class Post extends Component {

    constructor(props) {
        super(props);
        this.state = {
            isAuthenticated: false,
            currentUser: "",
            data: {},
            description: "",
            isLoading: true
        }

        var path = this.props.location.pathname;
        this.postId = path.substr(path.lastIndexOf('/') + 1);
        this.handleDeletePost = this.handleDeletePost.bind(this);
    }

    async componentDidMount() {
        await this.populateState();
        await this.handleData();
        
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            currentUser: user && user.name
        });
    }

    handleData = async() => {
        
        let response = await axios.get(`/api/Posts/Get?id=${this.postId}`)
        
        this.setState({ data: response.data, description: response.data.description });
        
        if (this.state.description == null) {
            this.setState({ description: "" })

        }

        this.setState({isLoading: false})
    }

    handleDeletePost = () => {
        axios.post(`/api/Posts/Delete?id=${this.postId}`)
            .then(result => {
                this.props.history.push("/user/" + this.state.data.creatorUserName)
                alert("Post Deleted!")
            })
            .catch(error => console.log(error));
        
    }

    render() {

        return(
            <React.Fragment>
            {this.state.isLoading && 
                <div>Loading... </div>}
            {!this.state.isLoading &&
                    <PostComponent state={this.state} data={this.state.data} postId={this.postId}
                    handleDelete={this.handleDeletePost} currentUser={this.state.currentUser}/>}
            </React.Fragment>
        );
    }
}