import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import { CreatePost } from './components/api-posts/CreatePost';
import { Post } from './components/api-posts/Post';
import { Profile } from './components/api-profiles/Profile';
import { AllMessages } from './components/api-messages/AllMessages';
import { PrivateMessage } from './components/api-messages/PrivateMessage';
import { Followers } from './components/api-profiles/Followers';
import { Followings } from './components/api-profiles/Followings';
import { Requests } from './components/api-profiles/Requests';
import { Notifications } from './components/api-notifications/Notifications';
import { NotFound } from './components/NotFound';
import { ToastContainer } from 'react-toastify';
import { Dashboard } from './components/api-admin/Dashboard';

import 'bootstrap';
import './custom.css'
import 'react-toastify/dist/ReactToastify.css';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
      <ToastContainer autoClose={8000} position='bottom-right' />
        <Route exact path='/' component={Home} />
        <Route exact path='/404' component={NotFound} />
        <AuthorizeRoute exact path='/adminpanel' component={Dashboard} />
        <AuthorizeRoute exact path='/adminpanel/manageusers' component={Profile} />
        <AuthorizeRoute exact path='/createpost' component={ CreatePost } />
        <AuthorizeRoute exact path='/user/:username' component={Profile} />
        <AuthorizeRoute exact path='/user/:username/followers' component={Followers} />
        <AuthorizeRoute exact path='/user/:username/followings' component={Followings} />
        <AuthorizeRoute exact path='/user/:username/requests' component={Requests} />
        <AuthorizeRoute exact path='/notifications' component={Notifications} />
        <AuthorizeRoute exact path='/post/:id' component={Post} />
        <AuthorizeRoute exact path='/messages' component={AllMessages} />
        <AuthorizeRoute exact path='/messages/u/:username' component={PrivateMessage} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
