import React, { Component } from 'react';
import ProfileComponent from './ProfileComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';

export class Profile extends Component {

    constructor(params) {
        super(params);
        this.state = {
            data: {},
            posts: [],
            isFollowing: false,
            currentUserName: "",
            btnText: "Follow",
            followersCount: 0
        }

        this.handleAction = this.handleAction.bind(this);
    }

    async componentDidMount() {
        await this.handleData();
        await this.handleFollowing();
    }

    handleData = async() => {
        let username = this.props.match.params.username;

        let response = await axios.get(`/api/Profiles/Get?username=${username}`)

        this.setState({data: response.data, posts: response.data.posts, followersCount: response.data.followersCount});
    }

    handleFollowing = async() => {

        let currentUser = await authService.getUser();
        let currentUserName = currentUser.name;
        this.setState({currentUserName: currentUserName});

         if (this.state.data.followers.some(f => f.followerUserName == currentUserName)) {
             this.setState({isFollowing: true, btnText:"Unfollow"});
         }
    }

    handleAddFollower = async() => {
        let currentUser = await authService.getUser();
        let followerId = currentUser.sub;

        let data = {
            userId: this.state.data.id,
            followerId: followerId
        };
        
        axios.post("/api/Profiles/Follow", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(result => {
            console.log(result);
            this.setState({btnText: "Unfollow", isFollowing: true, followersCount: this.state.followersCount + 1})
        })
        .catch(error => console.log(error));
    }

    handleRemoveFollower = async() => {
        let currentUser = await authService.getUser();
        let followerId = currentUser.sub;

        let data = {
            userId: this.state.data.id,
            followerId: followerId
        };
        
        axios.post("/api/Profiles/Unfollow", data, {
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(result => {
            console.log(result);
            this.setState({btnText: "Follow", isFollowing: false, followersCount: this.state.followersCount - 1})
        })
        .catch(error => console.log(error));
    }

    handleAction = () => {
        if (this.state.isFollowing) {
            this.handleRemoveFollower();
        } else {
            this.handleAddFollower();
        }
    }

    render() {
        return(
            <div>
            {this.state && this.state.data &&
            <ProfileComponent data={this.state.data} state={this.state}  handleAction={this.handleAction} />
            }
            </div>
        );
    }
}