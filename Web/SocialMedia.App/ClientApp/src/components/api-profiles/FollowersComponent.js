import React from 'react';
import { Link } from 'react-router-dom';

function FollowersComponent(params) {

    return (
        <React.Fragment>
            <div className="col-lg-4 offset-lg-5">
                {params.data.map(f =>
                    <div key={f.followerUserName} className="media user-follower">
                        <img src={f.followerProfilePictureUrl} alt="User Avatar" className="media-object pull-left" />
                        <div className="media-body">
                            <Link to={`/user/${f.followerUserName}`}>{f.followerFirstName}<br />
                                <span className="text-muted username">@{f.followerUserName}</span></Link>
                        </div>
                    </div>)}
                <Link to={`/user/${params.username}`}><button className="btn btn-default">Go back</button></Link>
            </div>
        </React.Fragment>
    );
}

export default FollowersComponent