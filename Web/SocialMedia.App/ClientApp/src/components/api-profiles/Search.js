import React, { Component } from 'react';
import SearchComponent from './SearchComponent';
import profileService from './ProfileService';

export class Search extends Component {
    constructor(props) {
        super(props);

        this.state = {
            query: "",
            results: []
        }

        this.handleChange = this.handleChange.bind(this);
        this.handleCloseOnClick = this.handleCloseOnClick.bind(this);
    }

    componentDidMount() {
        document.addEventListener("click", this.handleCloseOnClick);
    }

    handleChange = (event) => {
        const query = event.target.value;
        this.setState({ query: query })

        this.handleSearchResults(query);
    };

    handleSearchResults = async (query) => {
        if (query.length > 0) {
            await profileService.searchUsers(query)
                .then(r => {
                    this.setState({ results: r.data });
                    this.handleDisplayResults("block");
                })
                .catch(e => console.log(e));
        } else {
            this.setState({results: []});
            this.handleDisplayResults("none");
        }
    }

    handleDisplayResults = (style) => {
        const element = document.getElementById("search-results");

        element.style.display = style;
    }

    handleCloseOnClick = () => {
        const element = document.getElementById("search-results");
        element.style.display = "none";
    }

    render() {
        return (
            <SearchComponent handleChange={this.handleChange}
                results={this.state.results}
                query={this.state.query} />
        );
    }
}