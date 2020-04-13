import React from 'react';
import { Link } from 'react-router-dom';

function FollowersComponent(params) {

    return (
        <React.Fragment>
            <h1 className="offset-5">Followers</h1>
            <div className="col-lg-4 offset-lg-5">
                {params.data.length <= 0 ? <h5>No followers!</h5> :
                    params.data.map(f =>
                        <div key={f.followerUserName} className="media user-follower">
                            <img src={f.followerProfilePictureUrl} alt="User Avatar" className="media-object pull-left" />
                            <div className="media-body">
                                <Link to={`/user/${f.followerUserName}`}>{f.followerFirstName}<br />
                                    <span className="text-muted username">@{f.followerUserName}</span></Link>
                            </div>
                        </div>)}
            </div>
        </React.Fragment>
    );
}

export default FollowersComponent