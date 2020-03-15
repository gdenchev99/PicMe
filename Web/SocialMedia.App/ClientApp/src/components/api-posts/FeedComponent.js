import React from 'react';
import { Link } from "react-router-dom";

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
                            <div className="cardbox-base">
                                <ul className="float-right">
                                    <li><a><i className="fa fa-comments"></i></a></li>
                                    <li><a><em className="mr-5">12</em></a></li>
                                    <li><a><i className="fa fa-share-alt"></i></a></li>
                                    <li><a><em className="mr-3">03</em></a></li>
                                </ul>
                                <ul>
                                    <li><a><i className="fas fa-heart"></i></a></li>
                                    <li><a href="#"><img src="http://www.themashabrand.com/templates/bootsnipp/post/assets/img/users/3.jpeg" className="img-fluid rounded-circle" alt="User" /></a></li>
                                    <li><a><span>242 Likes</span></a></li>
                                </ul>
                            </div>
                            <div className="cardbox-comments">
                                <span className="comment-avatar float-left">
                                    <a href=""><img className="rounded-circle" src="http://www.themashabrand.com/templates/bootsnipp/post/assets/img/users/6.jpg" alt="..." /></a>
                                </span>
                                <div className="search">
                                    <input placeholder="Write a comment" type="text" />
                                    <button><i className="fa fa-camera"></i></button>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>)
            }
        </React.Fragment>
    );
}

export default FeedComponent