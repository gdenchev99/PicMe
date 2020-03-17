import React, { Component } from 'react';
import PostCommentsComponent from './PostCommentsComponent';
import axios from 'axios';

export class PostComments extends Component {

    constructor(props) {
        super(props);

        this.state = {
            data: [],
            isLoading: true
        }
    }

    async componentDidMount() {
        await this.handleData();
    }

    handleData = async() => {
        let postId = this.props.postId;

        let response = await axios.get(`/api/Comments/All?postId=${postId}`)

        this.setState({data: response.data});

        this.setState({isLoading: false})
    }

    render() {
        return(
            <React.Fragment>
                {this.state.isLoading ? <div>Loading....</div> : 
                <PostCommentsComponent data={this.state.data}/>}
                    
            </React.Fragment>
        );
    }
}