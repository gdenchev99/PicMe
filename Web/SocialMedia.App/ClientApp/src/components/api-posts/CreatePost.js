import React, { Component } from "react";
import CreatePostComponent from "./CreatePostComponent";
import authService from '../api-authorization/AuthorizeService';
import axios from 'axios';

export class CreatePost extends Component {

    constructor(params) {
        super(params);
        this.state = {
            creatorId: "",
            description: "",
            media: null,
            mediaSource: null,
            fileName: "Choose Image",
            errors: []
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
              fileName: event.target.files[0].name
            });
        }
    }

    handleData = async(event) => {
        event.preventDefault();

        let user = await authService.getUser();

        this.setState({ creatorId: user.sub })

        // let data = {
        //     creatorId: this.state.creatorId,
        //     description: this.state.description,
        //     mediaSource: this.state.mediaSource
        // };

        let data = new FormData();
        data.set("creatorId", this.state.creatorId);
        data.set("description", this.state.description);
        data.append("mediaSource", this.state.mediaSource);

        console.log(this.state.mediaSource);

        axios.post("/api/Posts/Create", data, {
            headers: {
                'Content-Type': 'multipart/form-data',
            }
        })
        .then(result => {
                this.setState({description: "", media: null})
            })
        .catch(errors => console.log(errors.response.data.errors));
    }
    

    render() {
        return(
            <div>
            <CreatePostComponent handleData={this.handleData} state={this.state} 
            handleChange={this.handleChange} handleMedia={this.handleMedia}/>
            </div>
        );
    }
}