import React from 'react';
import { Link } from "react-router-dom";
import { CreateComment } from "../api-comments/CreateComment";
import LikeComponent from '../api-likes/LikeComponent';

function FeedComponent(params) {
    return (
        <React.Fragment>
            {params.data.map(post => <div key={post.id} className="container">
                <div className="row">

                    <div className="col-lg-6 offset-lg-3">

                        <div className="cardbox shadow-lg bg-white">

                            <div className="cardbox-heading">

                                <div className="dropdown float-right">
                                    <button className="btn btn-flat btn-flat-icon" type="button" data-toggle="dropdown" aria-expanded="false">
                                        <em className="fa fa-ellipsis-h"></em>
                                    </button>
                                    <div className="dropdown-menu dropdown-scale dropdown-menu-right" role="menu">
                                        <a className="dropdown-item" href="#">Hide post</a>
                                        <a className="dropdown-item" href="#">Stop following</a>
                                        <a className="dropdown-item" href="#">Report</a>
                                    </div>
                                </div>
                                <div className="media m-0">
                                    <div className="d-flex mr-3">
                                    <Link to={"/user/" + post.creatorUserName}><img className="img-fluid rounded-circle" 
                                        src={post.creatorProfilePictureUrl} alt="User" /></Link>
                                    </div>
                                    <div className="media-body">
                                        <Link to={"/user/" + post.creatorUserName}><p className="m-0">{post.creatorFirstName} {post.creatorLastName}</p></Link>
                                        <small><span><i className="icon ion-md-pin"></i>{post.creatorUserName}</span></small>
                                        <small><span><i className="icon ion-md-time"></i>{post.createdOnFormat}</span></small>
                                    </div>
                                </div>
                            </div>

                            <div className="cardbox-item">
                                <img className="img-fluid" src={post.mediaSource} alt="Image" />
                            </div>
                            <LikeComponent />
                            <div className="view-comments">
                                <Link to="/post/2"><label>View all 12 comments</label></Link>
                                <p><span className="comments-username">Username</span> text</p>
                                <p><span className="comments-username">Username</span> text</p>
                            </div>
                            <CreateComment postId={post.id}/>
                        </div>

                    </div>
                </div>
            </div>)
            }
        </React.Fragment>
    );
}

export default FeedComponent