import React from 'react';
import { Link } from 'react-router-dom';

function FollowingsComponent(params) {

    return (
        <React.Fragment>
            <div className="col-lg-4 offset-lg-5">
                {params.data.map(f =>
                    <div key={f.userUserName} className="media user-follower">
                        <img src={f.userProfilePictureUrl} alt="User Avatar" className="media-object pull-left" />
                        <div className="media-body">
                            <Link to={`/user/${f.userUserName}`}>{f.userFirstName}<br />
                                <span className="text-muted username">@{f.userUserName}</span></Link>
                        </div>
                    </div>)}
                <Link to={`/user/${params.username}`}><button className="btn btn-default">Go back</button></Link>
            </div>
        </React.Fragment>
    );
}

export default FollowingsComponent