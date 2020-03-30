import React, { Fragment } from 'react';
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
                        {/* Begin post header */}
                        <div className="img-poster clearfix">
                            <img className="img-circle" src={params.data.creatorProfilePictureUrl} />
                            <strong><Link to={"/user/" + params.data.creatorUserName}>
                                {params.data.creatorUserName}</Link></strong>
                            {/* Start settings button for post (only post creator can see this) */}
                            {params.data.creatorUserName == params.currentUser ?
                                <Fragment>
                                <button className="btn btn-flat btn-flat-icon settings-icon" type="button" data-toggle="dropdown" aria-expanded="false">
                                    <em className="fa fa-ellipsis-h"></em>
                                </button>
                                <div className="dropdown-menu dropdown-scale dropdown-menu-right" role="menu">
                                    <button className="dropdown-item btn btn-flat" onClick={params.handleDelete}>Delete post</button>
                                    </div>
                                </Fragment>
                            : null}

                            {/* End settings button for post */}
                            <span>{params.data.createdOnFormat}</span>
                        </div>
                        {/* End post header */}
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
                                            <p id="description">{params.state.description}</p>
                                            <span className="date sub-text"> on {params.data.createdOnFormat}</span>
                                        </div>
                                    </li>
                                </ul>}
                            {/* Begin mapping comments here. */}
                            <ul className="img-comment-list">
                                <PostComments postId={params.postId} />
                            </ul>
                        </div>
                    </div>
                    {/* Import the create comment textarea and likes area*/}
                    <div className="modal-meta-bottom">
                        <Like postId={params.postId} />
                        <div className="post-comments-component"><CreateComment postId={params.postId} /></div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default PostComponent