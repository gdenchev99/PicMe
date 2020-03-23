import React, { Component } from 'react';
import CreateCommentComponent from './CreateCommentComponent';
import authService from '../api-authorization/AuthorizeService';
import axios from "axios";

export class CreateComment extends Component {

    constructor(params) {
        super(params);

        this.state = {
            text: "",
            postId: params.postId
        }

        this.handleData = this.handleData.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.handleAddEmoji = this.handleAddEmoji.bind(this);
        this.handleEmojiPicker = this.handleEmojiPicker.bind(this);
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

        let user = await authService.getUser();

        let userId = user.sub;

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
                console.log(result);
                this.setState({ text: "" })
            })
            .catch(errors => console.log(errors));
    }

    handleEmojiPicker = (event) => {
        let emojiPicker = document.getElementById("emoji-picker")
        
        if (emojiPicker.style.display == "block") {
            emojiPicker.style.display = "none";
        } else {
            emojiPicker.style.display = "block";
        }
    }

    render() {
        return (
            <CreateCommentComponent handleData={this.handleData} state={this.state}
                handleChange={this.handleChange} addEmoji={this.handleAddEmoji} showPicker={this.handleEmojiPicker} />
        );
    }
}