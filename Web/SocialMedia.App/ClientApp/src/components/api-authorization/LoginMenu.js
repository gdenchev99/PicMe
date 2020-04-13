import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';
import notificationsService from '../api-notifications/NotificationsService';
import { toast } from 'react-toastify';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            user: null,
            userName: null,
            unreadNotifications: 0
        };

        this.toastList = new Set();

        this.profileImgSrc = "https://res.cloudinary.com/dibntzvzk/image/upload/v1586118845/DefaultPhotos/user_gihmo1.png";
        this.messagesImgSrc = "https://res.cloudinary.com/dibntzvzk/image/upload/v1586119212/DefaultPhotos/send_gn4jza.png";
        this.createImgSrc = "https://res.cloudinary.com/dibntzvzk/image/upload/v1586121807/DefaultPhotos/addpost_wswvya.jpg";
        this.notificationsImgSrc = "https://res.cloudinary.com/dibntzvzk/image/upload/v1586440528/DefaultPhotos/heart_kldvsb.png";

    }

    async componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        await this.populateState();

        if (this.state.isAuthenticated) {
            await authService.refreshUser();
            await this.populateState();

            // Initiliaze the connection to the hub and set the connection Id to the current user id.
            await notificationsService.startConnection(this.state.user.sub);
            await this.handleUnreadNotifications();
            // Update the number of notifications when a new one is received.
            notificationsService._connection.on("ReceiveNotification",
                info => {
                    this.setState({ unreadNotifications: this.state.unreadNotifications + 1 });
                    this.notify(info);
                });
        }
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            user: user,
            userName: user && user.name
        });
    }

    async handleUnreadNotifications() {
        let unreadNotifications = await notificationsService.getUnreadNotifications(this.state.user.sub);
        this.setState({ unreadNotifications: unreadNotifications });
    }

    notify(info) {
        if (this.toastList.size < 2) {
            const id = toast.info(info, {
                onClose: () => this.toastList.delete(id)
            });
            this.toastList.add(id);
        }
    }

    render() {
        const { isAuthenticated, userName } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath);
        }
    }

    authenticatedView(userName, profilePath, logoutPath) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to="/notifications">
                    {this.state.unreadNotifications > 0 ?
                        <span className="badge badge-danger unread">{this.state.unreadNotifications}</span> : null}
                    <img height="23px" className="notification-icon" src={this.notificationsImgSrc} />
                </NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to="/createpost">
                    <img height="30px" src={this.createImgSrc} />
                </NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={`/user/${userName}`}>
                    <img height="20px" src={this.profileImgSrc} /></NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={`/messages`}>
                    <img height="20px" src={this.messagesImgSrc} />
                </NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={logoutPath}>Logout</NavLink>
            </NavItem>
        </Fragment>);

    }

    anonymousView(registerPath, loginPath) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={registerPath}>Register</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-dark" to={loginPath}>Login</NavLink>
            </NavItem>
        </Fragment>);
    }
}
