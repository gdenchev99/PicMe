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

        this.handleDelete = this.handleDelete.bind(this);
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
            currentUser: currentUser});
        
        this.setState({isLoading: false})
    }

    handleDelete = async(id) => {
        
        await axios.post(`/api/Comments/Delete?id=${id}`, null, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(res => {
                console.log(res)
            })
            .catch(e => console.log(e));
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
                isPostCreator={this.state.isPostCreator}
                handleDelete={this.handleDelete}/>}
                    
            </React.Fragment>
        );
    }
}