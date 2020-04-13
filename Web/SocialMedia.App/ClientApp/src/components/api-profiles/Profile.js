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
            isRequested: false,
            currentUser: null,
            currentUserName: "",
            btnText: "Follow",
            followersCount: 0,
            followingsCount: 0,
            profilePicture: ""
        }

        this.handleAction = this.handleAction.bind(this);
        this.handleMedia = this.handleMedia.bind(this);
    }

    async componentDidUpdate(prevProps, prevState) {
        if ((prevProps.match.params.username !== this.props.match.params.username) ||
            (prevState.data.id != this.state.data.id)) {
           await this.handleData();
           await this.handleFollowing();
           await this.handleIsPrivate();
        }
    }

    async componentDidMount() {
        let currentUser = await authService.getUser();
        let currentUserName = currentUser.name;
        this.setState({currentUser: currentUser, currentUserName: currentUserName});

        await this.handleData();

        if (this.state.data.id == undefined) {
            return this.props.history.push('/404');
        }

        await this.handleFollowing();
        await this.handleIsPrivate();
        
    }

    handleIsPrivate = async () => {
        let isFollowing = await profileService.isFollowing(this.state.currentUserName, this.state.data.followers);
        let isPrivate = this.state.data.isPrivate;

        if ((isFollowing && isPrivate == false) ||
            this.props.match.params.username == this.state.currentUserName) {
            this.setState({ isFollowing: true })
        }

    }

    handleData = async () => {
        let username = this.props.match.params.username;

        let profileResponse = await axios.get(`/api/Profiles/Get?username=${username}`);
        let postsResponse = await axios.get(`/api/Posts/Profile?username=${username}`);
        let followersCount = profileService.getFollowersCount(profileResponse.data.followers);
        let followingsCount = profileService.getFollowingsCount(profileResponse.data.followings);

        this.setState({
            data: profileResponse.data,
            profilePicture: profileResponse.data.profilePictureUrl,
            posts: postsResponse.data,
            postsCount: postsResponse.data.length,
            followersCount: followersCount,
            followingsCount: followingsCount
        });
    }

    handleFollowing = async () => {

        /* Reset the state before checking again */
        this.setState({isFollowing: false, isRequested: false})

        let isFollowing = await profileService.isFollowing(this.state.currentUserName, this.state.data.followers);
        
        let isRequested = await profileService.isRequested(this.state.currentUserName, this.state.data.followers);

        if(!isFollowing && !isRequested) {
            this.setState({isFollowing: isFollowing, isRequested: isRequested, btnText: "Follow"});
        }

        if (isFollowing) {
            this.setState({ isFollowing: isFollowing, btnText: "Unfollow" });
        }  
        
        if(isRequested) {
            this.setState({isRequested: isRequested ,btnText: "Requested"})
        }
        
    }

    handleAddFollower = async () => {

        let isFollowing = this.state.data.isPrivate ? false : true;
        let isRequested = !isFollowing;

        await profileService.addFollower(this.state.data.id, this.state.currentUser.sub)
            .then(() => {
                // If the profile is private, isRequested is set to true when following the user.
                isRequested ? 
                this.setState({ btnText: "Requested", isRequested: isRequested}) :
                this.setState({ btnText: "Unfollow", isFollowing: isFollowing, followersCount: this.state.followersCount + 1 })
            })
            .catch(error => console.log(error));
    }

    handleRemoveFollower = async () => {

        profileService.removeFollower(this.state.data.id, this.state.currentUser.sub)
            .then(() => {
                this.setState({ btnText: "Follow", 
                 isFollowing: false,
                 isRequested: false,
                 followersCount: this.state.followersCount - 1 })
                
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

    handleMedia = async (event) => {
        event.persist();

        if (event.target.files && event.target.files[0]) {
            let file = event.target.files[0];

            let data = new FormData();
            data.append("picture", file);
            data.set("username", this.state.currentUserName);

            this.setState({ loading: true })
            await axios.post('/api/Profiles/ProfilePicture', data, {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
                .then(result => this.setState({ profilePicture: result.data }));
        }
    }

    render() {
        return (
            <div>
                {this.state && this.state.data &&
                    <ProfileComponent key={this.state.data.id}
                        data={this.state.data}
                        state={this.state}
                        isFollowing={this.state.isFollowing}
                        handleAction={this.handleAction}
                        handleMedia={this.handleMedia} />
                }
            </div>
        );
    }
}