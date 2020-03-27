import React from 'react';
import { Link } from "react-router-dom";

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
										<h3 className="mb-0 text-truncated">{params.data.userName}</h3>
										<p className="lead">{params.data.firstName + " " + params.data.lastName}</p>
										<p>
											I love to read, hang out with friends, watch football, listen to music, and learn new things.
                           			    </p>
									</div>
									<div className="col-12 col-lg-2 col-md-6 text-center">
										<img src={params.data.profilePictureUrl} alt="" className="mx-auto rounded-circle img-fluid" />

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
										<h5>{params.data.followingsCount}</h5>

									</div>
									<div className="col-12 col-lg-4">
										<h3 className="mb-0">Posts</h3>
										<h5>{params.state.postsCount}</h5>
									</div>
								</div>
							</div>
						</div>
					</div>

					{params.state.posts.map(post =>
						<div key={post.id} className="gallery_product col-lg-4 col-md-4 col-sm-4 col-xs-6 filter hdpe">
							<Link to={`/post/${post.id}`}>
								{post.mediaSource.substr(post.mediaSource.lastIndexOf('.')) === ".mp4" ?
									<video src={post.mediaSource} className="img-responsive gallery-images" /> :
									<img src={post.mediaSource} className="img-responsive gallery-images" />}
							</Link>
						</div>)}
				</div>
			</div>
		</React.Fragment>
	);
}

export default ProfileComponent