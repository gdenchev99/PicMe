import React, { Component } from 'react';
import { Feed } from './api-posts/Feed';
import authService from './api-authorization/AuthorizeService';

export class Home extends Component {
  static displayName = Home.name;
  
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false
    }
  }

  componentDidMount() {
    this.getAuthentication();
  }

  async getAuthentication() {
    const isAuthenticated = await authService.isAuthenticated();
    this.setState({isAuthenticated: isAuthenticated});
  }

  render () {
    return (
      this.state.isAuthenticated ?
      <Feed /> :
      <div><center><h1>Hello! Please login in order to proceed!</h1></center></div>
    );
  }
}
