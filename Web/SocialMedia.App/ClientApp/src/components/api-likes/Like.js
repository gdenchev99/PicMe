import React, { Component } from 'react';
import LikeComponent from './LikeComponent';
import authService from '../api-authorization/AuthorizeService';
import { svgParams } from './LikeConstants';
import notificationsService from '../api-notifications/NotificationsService';
import likesService from './LikesService';

export class Like extends Component {

    constructor(props) {
        super(props);

        this.state = {
            isLiked: false,
            user: null,
            userId: "",
            svgPath: "",
            svgFill: "",
            postId: props.postId,
            pictures: [],
            likesCount: 0
        }

        this.connection = null;

        this.handleClick = this.handleClick.bind(this);
    }

    async componentDidMount() {
        let user = await authService.getUser();
        let userId = user.sub;
        this.setState({ user: user, userId: userId });

        await this.handleIsLiked();

        this.handleLatestLikes();

        if (this.state.isLiked) {
            this.setState({
                svgPath: svgParams.redHeartPath,
                svgFill: "#ff0000"
            });
        } else {
            this.setState({
                svgPath: svgParams.emptyHeartPath,
                svgFill: ""
            });
        }
    }

    handleClick = () => {
        if (this.state.isLiked) {
            this.setState({ svgPath: "M34.3 3.5C27.2 3.5 24 8.8 24 8.8s-3.2-5.3-10.3-5.3C6.4 3.5.5 9.9.5 17.8s6.1 12.4 12.2 17.8c9.2 8.2 9.8 8.9 11.3 8.9s2.1-.7 11.3-8.9c6.2-5.5 12.2-10 12.2-17.8 0-7.9-5.9-14.3-13.2-14.3zm-1 29.8c-5.4 4.8-8.3 7.5-9.3 8.1-1-.7-4.6-3.9-9.3-8.1-5.5-4.9-11.2-9-11.2-15.6 0-6.2 4.6-11.3 10.2-11.3 4.1 0 6.3 2 7.9 4.2 3.6 5.1 1.2 5.1 4.8 0 1.6-2.2 3.8-4.2 7.9-4.2 5.6 0 10.2 5.1 10.2 11.3 0 6.7-5.7 10.8-11.2 15.6z", svgFill: "" });
            this.handleRemoveLike();
        } else {
            this.setState({
                svgPath: "M35.3 35.6c-9.2 8.2-9.8 8.9-11.3 8.9s-2.1-.7-11.3-8.9C6.5 30.1.5 25.6.5 17.8.5 9.9 6.4 3.5 13.7 3.5 20.8 3.5 24 8.8 24 8.8s3.2-5.3 10.3-5.3c7.3 0 13.2 6.4 13.2 14.3 0 7.8-6.1 12.3-12.2 17.8z",
                svgFill: "#ff0000"
            });
            this.handleAddLike();
        }
        this.setState({ isLiked: !this.state.isLiked });
    };

    handleAddLike = async () => {
        await likesService.addLike(this.state.userId, this.state.postId)
            .then(result => {
                this.setState({ likesCount: this.state.likesCount + 1 })
                // Get the post's creator's id from the result.
                let posterId = result.data;
                // Id of the post.
                let postId = Number(this.state.postId);
                // Get the username of the current user.
                let username = this.state.user.name;
                // The message of the notification
                let info = `@${username} liked your post.`;
                // Notify the creator of the post that his post has been liked by the current user.
                // Arg1 - id of the post's creator, Arg2 - id of the post, Ar3 - instant notification message.
                notificationsService.invokeNotificationMessage(posterId, postId, info);
            })
            .catch(error => console.log(error));
    }

    handleRemoveLike = async () => {
        await likesService.removeLike(this.state.userId, this.state.postId)
            .then(result => {
                this.setState({ likesCount: this.state.likesCount - 1 })
            })
            .catch(error => console.log(error));
    }

    handleIsLiked = async () => {

        let isLiked = await likesService.isLiked(this.state.userId, this.state.postId);

        this.setState({ isLiked: isLiked });
    }

    handleLatestLikes = async () => {

        var pictures = await likesService.latestLikes(this.state.postId);
        this.setState({ pictures: pictures, likesCount: pictures.length })
    }

    render() {
        return (
            <LikeComponent state={this.state} handleClick={this.handleClick} />
        );
    }
}