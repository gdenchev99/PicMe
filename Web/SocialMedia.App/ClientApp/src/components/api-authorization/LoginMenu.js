import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null
        };

        this.profileImgSrc = "https://res.cloudinary.com/dibntzvzk/image/upload/v1586118845/DefaultPhotos/user_gihmo1.png";
        this.messagesImgSrc = "https://res.cloudinary.com/dibntzvzk/image/upload/v1586119212/DefaultPhotos/send_gn4jza.png";
        this.createImgSrc = "https://res.cloudinary.com/dibntzvzk/image/upload/v1586121807/DefaultPhotos/addpost_wswvya.jpg";
    }

    async componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        await this.populateState();

        if(this.state.isAuthenticated) {
            await authService.refreshUser();
            this.populateState();
        }
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userName: user && user.name
        });
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
