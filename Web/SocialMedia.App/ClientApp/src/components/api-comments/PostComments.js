import React, { Component } from 'react';
import PostCommentsComponent from './PostCommentsComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';
import commentsService from './CommentsService';

export class PostComments extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: [],
            currentUser: "",
            isPostCreator: false,
            isLoading: true,
            skipCount: 0,
            takeCount: 4
        }

        this.handleDelete = this.handleDelete.bind(this);
        this.loadMore = this.loadMore.bind(this);
    }

    async componentDidMount() {
        await this.handleData();
        await this.handleCreatorDelete();
    }

    handleData = async () => {
        let postId = this.props.postId;
        let user = await authService.getUser();
        let currentUser = user.name;

        let response = await commentsService.getComments(postId, this.state.skipCount, this.state.takeCount);

        this.setState({
            data: response.data,
            currentUser: currentUser,
            skipCount: this.state.skipCount + this.state.takeCount
        });

        this.setState({ isLoading: false })
    }

    loadMore = async () => {
        let postId = this.props.postId;

        let response = await commentsService.getComments(postId, this.state.skipCount, this.state.takeCount);

        if (response.data.length > 0) {
            this.setState({ data: this.state.data.concat(response.data), skipCount: this.state.skipCount + this.state.takeCount });
        }
    }

    handleDelete = async (id) => {
        await commentsService.deleteComment(id)
            .then(res => {
                console.log(res);
                document.getElementById(id).style.display = "none"; // Remove the comment from the view.
            })
            .catch(e => console.log(e));
    }

    handleCreatorDelete = async () => {
        let postCreator = this.props.postCreator;
        let user = await authService.getUser();
        let currentUser = user.name;

        if (postCreator === currentUser) {
            this.setState({ isPostCreator: true })
        }
    }

    render() {
        return (
            <React.Fragment>
                {this.state.isLoading ? <div>Loading....</div> :
                    <PostCommentsComponent data={this.state.data}
                        currentUser={this.state.currentUser}
                        isPostCreator={this.state.isPostCreator}
                        handleDelete={this.handleDelete}
                        loadMore={this.loadMore} />}

            </React.Fragment>
        );
    }
}