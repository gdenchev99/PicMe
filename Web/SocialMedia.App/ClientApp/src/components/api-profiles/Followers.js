import React, { Component } from 'react';
import FollowersComponent from './FollowersComponent';

export class Followers extends Component {

    constructor(props) {
        super(props);
        console.log(props);
        
    }

    render() {
        return (
            <FollowersComponent />
        );
    }
}