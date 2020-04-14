import React, { Component, createRef } from 'react';
import ReactDOM from 'react-dom'
import CreateCommentComponent from './CreateCommentComponent';
import authService from '../api-authorization/AuthorizeService';
import axios from "axios";
import notificationsService from '../api-notifications/NotificationsService';

export class CreateComment extends Component {

    constructor(params) {
        super(params);

        this.state = {
            text: "",
            postId: params.postId,
            user: null
        }

        this.handleData = this.handleData.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleAddEmoji = this.handleAddEmoji.bind(this);
        this.handleEmojiPicker = this.handleEmojiPicker.bind(this);
        this.handleCloseOnClick = this.handleCloseOnClick.bind(this);

        this.emojiRef = React.createRef();
    }

    async componentDidMount() {
        let user = await authService.getUser();
        this.setState({ user: user });

        document.addEventListener("mousedown", this.handleCloseOnClick);
    }

    handleAddEmoji = e => {
        let emoji = e.native;
        this.setState({
            text: this.state.text + emoji
        });
    };

    handleChange = ({ target }) => {
        this.setState({ [target.name]: target.value });
    };

    handleData = async (event) => {
        event.preventDefault();

        let userId = this.state.user.sub;

        let data = {
            creatorId: userId,
            postId: this.state.postId,
            text: this.state.text
        };

        axios.post("api/Comments/Create", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
            .then(result => {
                this.setState({ text: "" });
                // Post's creator's id.
                let posterId = result.data;
                // Id of the post.
                let postId = Number(this.state.postId);
                // Username of the currently logged-in user.
                let username = this.state.user.name;
                // Notification info.
                let info = `@${username} commented on your post.`;
                // Notify the creator of the post that the current user has commented.
                // Arg1 - id of the post's creator, Arg2 - id of the post, Ar3 - instant notification message.
                notificationsService.invokeNotificationMessage(posterId, postId, info);
            })
            .catch(errors => console.log(errors));
    }

    handleEmojiPicker = (event) => {
        if (this.emojiRef.current.style.display == "block") {
            this.emojiRef.current.style.display = "none";
        } else {
            this.emojiRef.current.style.display = "block";
        }
    }

    // Close the emoji box when you click outside of it.
    handleCloseOnClick = (event) => {
        try {
            let node = ReactDOM.findDOMNode(this.emojiRef.current);
            if (!node.contains(event.target)) {
                // Handle outside click here
                node.style.display = "none";
            }
        } catch (error) {
            return null;
        }
    }

    render() {
        return (
            <CreateCommentComponent handleData={this.handleData} state={this.state}
                handleChange={this.handleChange}
                addEmoji={this.handleAddEmoji}
                showPicker={this.handleEmojiPicker}
                emojiRef={this.emojiRef} />
        );
    }
}