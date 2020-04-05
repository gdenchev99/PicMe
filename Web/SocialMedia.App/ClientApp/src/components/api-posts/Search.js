import React, { Component } from 'react';

export class Search extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="searchBar">
                <input type="text" placeholder="Search..." />
                <i className="fas fa-search search-icon" />
            </div>
        );
    }
}