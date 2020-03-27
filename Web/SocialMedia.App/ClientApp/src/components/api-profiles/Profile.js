import React, { Component } from 'react';
import ProfileComponent from './ProfileComponent';
import axios from 'axios';
import authService from '../api-authorization/AuthorizeService';
import profileService from './ProfileService';

export class Profile extends Component {

    constructor(props) {
        super(props);
        this.state = {
            data: {},
            posts: [],
            postsCount: 0,
            isFollowing: false,
            currentUserName: "",
            btnText: "Follow",
            followersCount: 0
        }

        this.handleAction = this.handleAction.bind(this);
    }

    componentDidUpdate(prevProps) {
        if (prevProps.match.params.username !== this.props.match.params.username) {
            this.handleData();
        }
    }

    async componentDidMount() {
        await this.handleData();
        await this.handleFollowing();
    }

    handleData = async() => {
        let username = this.props.match.params.username;

        let profileResponse = await axios.get(`/api/Profiles/Get?username=${username}`);
        let postsResponse = await axios.get(`/api/Posts/Profile?username=${username}`);

        this.setState({
            data: profileResponse.data,
            posts: postsResponse.data,
            postsCount: postsResponse.data.length,
            followersCount: profileResponse.data.followersCount
        });
    }

    handleFollowing = async() => {

        let currentUser = await authService.getUser();
        let currentUserName = currentUser.name;
        this.setState({ currentUserName: currentUserName });
        
        let isFollowing = await profileService.isFollowing(currentUserName, this.state.data.followers);

         if (isFollowing) {
             this.setState({isFollowing: isFollowing, btnText:"Unfollow"});
         }
    }

    handleAddFollower = async() => {
        let currentUser = await authService.getUser();
        let followerId = currentUser.sub;
        
        profileService.addFollower(this.state.data.id, followerId)
            .then(() => {
                this.setState({ btnText: "Unfollow", isFollowing: true, followersCount: this.state.followersCount + 1 })
            })
            .catch(error => console.log(error));
    }

    handleRemoveFollower = async() => {
        let currentUser = await authService.getUser();
        let followerId = currentUser.sub;

        profileService.removeFollower(this.state.data.id, followerId)
        .then(() => {
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