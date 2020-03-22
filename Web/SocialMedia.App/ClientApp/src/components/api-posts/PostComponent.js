import React from 'react';
import { CreateComment } from '../api-comments/CreateComment';
import LikeComponent from '../api-likes/LikeComponent';
import { PostComments } from '../api-comments/PostComments';
import { Link } from "react-router-dom";
import { Like } from '../api-likes/Like';

function PostComponent(params) {

    return (
        <div className="card mb-3">
            <div className="row no-gutters">
                <div className="col-md-8 modal-image">
                    <img className="img-responsive" src={params.data.mediaSource} />
                </div>
                <div className="col-md-4 modal-meta">
                    <div className="modal-body modal-meta-top">
                        <div className="img-poster clearfix">
                            <img className="img-circle" src={params.data.creatorProfilePictureUrl} />
                            <strong><Link to={"/user/" + params.data.creatorUserName}>
                                {params.data.creatorUserName}</Link></strong>
                            <span>{params.data.createdOnFormat}</span>
                        </div>
                        <hr />
                        <div>
                            {/* First ul element is the description if it exists. */}
                            {params.state.description.length <= 0 ? null :
                                <ul className="img-comment-list">
                                    <li>
                                        <div className="comment-img">
                                            <img src={params.data.creatorProfilePictureUrl} />
                                        </div>
                                        <div className="comment-text">
                                            <strong><Link to={"/user/" + params.data.creatorUserName}>
                                                {params.data.creatorUserName}</Link></strong>
                                            <p>{params.state.description}</p> <span className="date sub-text">
                                                on {params.data.createdOnFormat}</span>
                                        </div>
                                    </li>
                                </ul>}
                            {/* Begin mapping comments here. */}
                            <ul className="img-comment-list">
                            <PostComments postId={params.postId}/>
                            </ul>
                        </div>
                    </div>
                            {/* Import the create comment textarea */}
                    <div className="modal-meta-bottom">
                        <Like postId={params.postId} />
                        <CreateComment postId={params.postId}/>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PostComponent