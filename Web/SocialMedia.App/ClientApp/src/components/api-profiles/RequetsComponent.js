import React from 'react';
import { Link } from 'react-router-dom';

function RequestComponent(params) {
    return (
        <React.Fragment>
            <h1 className="offset-5">Requests</h1>
            <div className="col-lg-4 offset-lg-4">
                {params.data.length <= 0 ? <h5 className="offset-4">No requests!</h5> :
                    params.data.map(f =>
                        <div key={f.followerUserName} className="media user-follower">
                            <img src={f.followerProfilePictureUrl} alt="User Avatar" className="media-object pull-left" />
                            <div className="media-body">
                                <Link to={`/user/${f.followerUserName}`}>{f.followerFirstName}<br />
                                    <span className="text-muted username">@{f.followerUserName}</span></Link>
                                <button onClick={(ev) => params.handleApprove(f.followerUserName, ev)}
                                    className="btn btn-primary btn-approve">Approve</button>
                                <button onClick={(ev) => params.handleDelete(f.followerUserName, ev)}
                                    className="btn btn-default">Delete</button>
                            </div>
                        </div>)}
            </div>
        </React.Fragment>
    );
}

export default RequestComponent