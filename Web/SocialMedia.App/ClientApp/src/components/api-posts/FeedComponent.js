import React from 'react';
import { Link } from "react-router-dom";
import { CreateComment } from "../api-comments/CreateComment";
import { Like } from '../api-likes/Like';
import { FeedComments } from '../api-comments/FeedComments';
import InfiniteScroll from 'react-infinite-scroll-component';
import {Image, Video} from 'cloudinary-react';

function FeedComponent(params) {
    return (
        <React.Fragment>
            {params.data.length <= 0 ?
                <h2>No posts here, please follow people in order to see what they post!</h2> :
                /* Infinite scroller config */
                <InfiniteScroll dataLength={params.data.length} next={params.loadMore} hasMore={true}>
                    {params.data.map(post => <div key={post.id} className="container">
                        <div className="row">

                            <div className="col-lg-7 offset-lg-2">

                                <div className="cardbox shadow-lg bg-white">

                                    <div className="cardbox-heading">

                                        <div className="media m-0">
                                            <div className="d-flex mr-3">
                                                <Link to={"/user/" + post.creatorUserName}><img className="img-fluid rounded-circle"
                                                    src={post.creatorProfilePictureUrl} alt="User" /></Link>
                                            </div>
                                            <div className="media-body">
                                                <Link to={"/user/" + post.creatorUserName}><p className="m-0">{post.creatorFirstName} {post.creatorLastName}</p></Link>
                                                <small><span><i className="icon ion-md-pin"></i>{post.creatorUserName}</span></small>
                                                <Link to={`/post/${post.id}`}>
                                                    <small><span><i className="icon ion-md-time"></i>{post.createdOnFormat}</span></small>
                                                </Link>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="cardbox-item">
                                        {post.mediaSource.substr(post.mediaSource.lastIndexOf('.')) === ".mp4" ?
                                            <Video cloudName="dibntzvzk" publicId={post.mediaPublicId} 
                                            controls="true" className="img-responsive gallery-images" /> :
                                            <Image cloudName="dibntzvzk" publicId={post.mediaPublicId} 
                                            className="img-responsive gallery-images"/>}
                                    </div>
                                    <Like postId={post.id} />
                                    <FeedComments postId={post.id} commentsCount={post.commentsCount} />
                                    <div className="feed-comments-component"><CreateComment postId={post.id} /></div>
                                </div>

                            </div>
                        </div>
                    </div>)}
                </InfiniteScroll>
            }
        </React.Fragment>
    );
}

export default FeedComponent