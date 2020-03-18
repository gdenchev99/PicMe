import React, { Component } from 'react';
import axios from 'axios';
import FeedCommentsComponent from './FeedCommentsComponent';

export class FeedComments extends Component {

    constructor(props) {
        super(props);
        this.state = {
            data: [],
            isLoading: true,
            count: 0
        }
    }

    async componentDidMount() {
        await this.handleData();
    }

    handleData = async () => {
        let postId = this.props.postId;

        let response = await axios.get(`/api/Comments/Feed?postId=${postId}`)
        
        this.setState({ data: response.data, count: this.props.commentsCount });

        this.setState({ isLoading: false })
    }

    render() {
        return (
            <React.Fragment>
                {this.state.isLoading ? <div>Loading....</div> :
                    <FeedCommentsComponent data={this.state.data} postId={this.props.postId} count={this.state.count}/>}
            </React.Fragment>
        );
    }
}