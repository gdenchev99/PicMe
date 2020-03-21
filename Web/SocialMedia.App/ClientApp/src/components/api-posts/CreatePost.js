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
            this.setState({
              media: URL.createObjectURL(event.target.files[0]),
              fileName: event.target.files[0].name
            });
        }
    }

    handleData = async(event) => {
        event.preventDefault();

        let user = await authService.getUser();

        this.setState({ creatorId: user.sub })

        let data = JSON.stringify({
            creatorId: this.state.creatorId,
            description: this.state.description,
            mediaSource: this.state.media
        });

        axios.post("/api/Posts/Create", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
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