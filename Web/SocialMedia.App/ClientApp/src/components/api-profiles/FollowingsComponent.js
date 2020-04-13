import React from 'react';
import { Link } from 'react-router-dom';

function FollowingsComponent(params) {

    return (
        <React.Fragment>
            <h1 className="offset-5">Followings</h1>
            <div className="col-lg-4 offset-lg-5">
                {params.data.length <= 0 ? <h5>No followings!</h5> :
                    params.data.map(f =>
                        <div key={f.userUserName} className="media user-follower">
                            <img src={f.userProfilePictureUrl} alt="User Avatar" className="media-object pull-left" />
                            <div className="media-body">
                                <Link to={`/user/${f.userUserName}`}>{f.userFirstName}<br />
                                    <span className="text-muted username">@{f.userUserName}</span></Link>
                            </div>
                        </div>)}
            </div>
        </React.Fragment>
    );
}

export default FollowingsComponent