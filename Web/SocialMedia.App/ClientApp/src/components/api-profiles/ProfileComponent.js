import React from 'react';
import { Link } from "react-router-dom";
import { Image, Video } from 'cloudinary-react';

function ProfileComponent(params) {

	return (
		<React.Fragment>

			<div className="container">
				<div className="row">

					<div className="col-12">
						<div className="card">
							<div className="card-body">
								<div className="row">
									<div className="col-12 col-lg-10 col-md-6">
										<h3 className="mb-0 text-truncated">{params.data.userName}
											{params.data.userName === params.state.currentUserName ? null :
												<Link to={`/messages/u/${params.data.userName}`}>
													<button className="btn btn-primary msgbtn">Message</button></Link>}
										</h3>
										<p className="lead">{params.data.firstName + " " + params.data.lastName}</p>
										<p>
											{params.data.bio}
										</p>
									</div>
									<div className="col-12 col-lg-2 col-md-6 text-center">
										<div>
											<label htmlFor="img-input">
												<img src={params.state.profilePicture}
													className="mx-auto rounded-circle img-fluid profile-picture" />
											</label>
											{params.data.userName === params.state.currentUserName ?
												<input id="img-input" type="file" onChange={params.handleProfilePicture} hidden /> : null}
										</div>

										{params.data.userName === params.state.currentUserName ?
											<Link to={`/authentication/profile`}>
												<button className="btn btn-primary fbtwn-margin">Settings</button></Link> :
											<button onClick={params.handleAction} className="btn btn-block btn-outline-success fwbtn-margin"><span className="fa fa-plus-circle"></span>{params.state.btnText}</button>}

									</div>
									<div className="col-12 col-lg-4">
										<Link to={`${params.data.userName}/followers`}><h3 className="mb-0">Followers</h3></Link>
										<h5>{params.state.followersCount}</h5>
									</div>
									<div className="col-12 col-lg-4">
										<Link to={`${params.data.userName}/followings`}><h3 className="mb-0">Following</h3></Link>
										<h5>{params.state.followingsCount}</h5>

									</div>
									<div className="col-12 col-lg-4">
										<h3 className="mb-0">Posts</h3>
										<h5>{params.state.postsCount}</h5>
									</div>
								</div>
							</div>
						</div>
					</div>

					{(params.isFollowing === false && params.data.isPrivate) ?
						<div className="offset-lg-3"><h3>This profile is private, please follow the user!</h3></div> :
						params.state.posts.map(post =>
							<div key={post.id} className="gallery_product col-lg-4 col-md-4 col-sm-4 col-xs-6 filter hdpe">
								<Link to={`/post/${post.id}`}>
									{post.mediaExtension === ".mp4" ?
										<Video cloudName="dibntzvzk" publicId={post.mediaPublicId}
											className="img-responsive gallery-images" /> :
										<Image cloudName="dibntzvzk" publicId={post.mediaPublicId}
											className="img-responsive gallery-images" />}
								</Link>
							</div>)}
				</div>
			</div>
		</React.Fragment>
	);
}

export default ProfileComponent