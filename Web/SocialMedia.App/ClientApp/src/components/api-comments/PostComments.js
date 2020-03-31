import React, { Component } from 'react';
import PostCommentsComponent from './PostCommentsComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';

export class PostComments extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: [],
            currentUser: "",
            isPostCreator: false,
            isLoading: true
        }
    }

    async componentDidMount() {
        await this.handleData();
        await this.handleCreatorDelete();
    }

    handleData = async() => {
        let postId = this.props.postId;
        let user = await authService.getUser();
        let currentUser = user.name; 

        let response = await axios.get(`/api/Comments/All?postId=${postId}`)

        this.setState({data: response.data, 
            totalComments: response.data.length, 
            currentUser: currentUser});
        
        this.setState({isLoading: false})
    }

    handleCreatorDelete = async () => {
        let postCreator = this.props.postCreator;
        let user = await authService.getUser();
        let currentUser = user.name; 

        if(postCreator === currentUser) {
            this.setState({isPostCreator: true})
        }
    }

    render() {
        return(
            <React.Fragment>
                {this.state.isLoading ? <div>Loading....</div> : 
                <PostCommentsComponent data={this.state.data}
                currentUser={this.state.currentUser}
                isPostCreator={this.state.isPostCreator}/>}
                    
            </React.Fragment>
        );
    }
}