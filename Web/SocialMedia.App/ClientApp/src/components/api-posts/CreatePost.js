import React, { Component } from "react";
import CreatePostComponent from "./CreatePostComponent";
import authService from '../api-authorization/AuthorizeService';
import postsService from "./PostsService";

export class CreatePost extends Component {

    constructor(params) {
        super(params);
        this.state = {
            creatorId: "",
            description: "",
            media: null,
            mediaSource: null,
            fileName: "Choose Image",
            errors: [],
            loading: false
        };

        this.handleChange = this.handleChange.bind(this)
        this.handleMedia = this.handleMedia.bind(this);
        this.handleData = this.handleData.bind(this);
    }

    handleChange = ({ target }) => {
        this.setState({ [target.name]: target.value });
    };

    handleMedia = (event) => {

        if (event.target.files && event.target.files[0]) {
            let file = event.target.files[0];
            this.setState({
                media: URL.createObjectURL(file),
                mediaSource: file,
                fileName: file.name
            });
        }
    }

    handleData = async (event) => {
        event.preventDefault();

        this.setState({ loading: true })

        let user = await authService.getUser();

        this.setState({ creatorId: user.sub })

        await postsService.createPost(this.state.creatorId, this.state.description, this.state.mediaSource)
            .then(() => {
                this.setState({ description: "", media: null, mediaSource: null, loading: false, fileName: "Choose Image" })
            })
            .catch(errors => {
                this.setState({ errors: errors.response.data.errors, loading: false });
            });
    }


    render() {
        return (
            <div>
                <CreatePostComponent handleData={this.handleData} state={this.state}
                    handleChange={this.handleChange} handleMedia={this.handleMedia} />
            </div>
        );
    }
}