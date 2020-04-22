import React, { Component } from 'react';
import ProfileComponent from './ProfileComponent';
import authService from '../api-authorization/AuthorizeService';
import profileService from './ProfileService';
import notificationsService from '../api-notifications/NotificationsService';

export class Profile extends Component {

    constructor(props) {
        super(props);
        this.state = {
            error: null,
            loading: false,
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
        this.handleProfilePicture = this.handleProfilePicture.bind(this);
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
        this.setState({ currentUser: currentUser, currentUserName: currentUserName });

        await this.handleData();

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

        this.setState({loading: true});

        let profileResponse = await profileService.getProfile(username)
        .catch(error => {
            this.setState({error: error.response.data.message});
        });

        let postsResponse = await profileService.getProfilePosts(username);

        let followersCount = profileService.getFollowersCount(profileResponse.data.followers);
        let followingsCount = profileService.getFollowingsCount(profileResponse.data.followings);

        this.setState({
            data: profileResponse.data,
            profilePicture: profileResponse.data.profilePictureUrl,
            posts: postsResponse.data,
            postsCount: postsResponse.data.length,
            followersCount: followersCount,
            followingsCount: followingsCount,
            loading: false
        });
    }

    handleFollowing = async () => {

        /* Reset the state before checking again */
        this.setState({ isFollowing: false, isRequested: false })

        let isFollowing = await profileService.isFollowing(this.state.currentUserName, this.state.data.followers);

        let isRequested = await profileService.isRequested(this.state.currentUserName, this.state.data.followers);

        if (!isFollowing && !isRequested) {
            this.setState({ isFollowing: isFollowing, isRequested: isRequested, btnText: "Follow" });
        }

        if (isFollowing) {
            this.setState({ isFollowing: isFollowing, btnText: "Unfollow" });
        }

        if (isRequested) {
            this.setState({ isRequested: isRequested, btnText: "Requested" })
        }

    }

    handleAddFollower = async () => {

        let isFollowing = this.state.data.isPrivate ? false : true;
        let isRequested = !isFollowing;

        await profileService.addFollower(this.state.data.id, this.state.currentUser.sub)
            .then(() => {
                let receiverId = this.state.data.id;
                let postId = null;
                let username = this.state.currentUserName;
                // If the profile is private, isRequested is set to true when following the user.
                if (isRequested) {
                    this.setState({ btnText: "Requested", isRequested: isRequested })
                    let info = `@${username} has requested to follow you.`;
                    notificationsService.invokeNotificationMessage(receiverId, postId, info);
                } else {
                    this.setState({ btnText: "Unfollow", isFollowing: isFollowing, followersCount: this.state.followersCount + 1 })
                    let info = `@${username} followed you.`;
                    notificationsService.invokeNotificationMessage(receiverId, postId, info);
                }
            })
            .catch(error => console.log(error));
    }

    handleRemoveFollower = async () => {

        profileService.removeFollower(this.state.data.id, this.state.currentUser.sub)
            .then(() => {
                this.setState({
                    btnText: "Follow",
                    isFollowing: false,
                    isRequested: false,
                    followersCount: this.state.followersCount - 1
                })

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

    handleProfilePicture = async (event) => {
        event.persist();

        if (event.target.files && event.target.files[0]) {
            let file = event.target.files[0];

            await profileService.uploadProfilePicture(this.state.currentUser.sub, file)
                    .then(result => this.setState({ profilePicture: result.data }));
        }
    }

    render() {
        
        if(this.state.error) {
            this.props.history.push("/404");
        }

        if(this.state.loading) {
            return (
            <div className="row">
                <div className="col-2 offset-5 text-primary"><h1>Loading...</h1></div>
                <div className="spinner-grow spn text-primary"></div>
            </div>);
        }

        return (
            <React.Fragment>
            <div>
                {this.state && this.state.data &&
                    <ProfileComponent key={this.state.data.id}
                        data={this.state.data}
                        state={this.state}
                        isFollowing={this.state.isFollowing}
                        handleAction={this.handleAction}
                        handleProfilePicture={this.handleProfilePicture} />
                }
            </div>
            </React.Fragment>
        );
    }
}