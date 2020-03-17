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

import 'bootstrap';
import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <AuthorizeRoute exact path='/createpost' component={ CreatePost } />
        <AuthorizeRoute exact path='/user/:username' component={ Profile } />
        <AuthorizeRoute exact path='/post/:id' component={ Post } />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
