import React from 'react';

function FollowersComponent(params) {

    return (
        <React.Fragment>
            <div className="col-lg-4 offset-lg-4">
                <div className="media user-follower">
                    <img src="https://bootdey.com/img/Content/avatar/avatar1.png" alt="User Avatar" className="media-object pull-left" />
                    <div className="media-body">
                        <a href="#">Antonius<br /><span className="text-muted username">@mrantonius</span></a>
                        <button type="button" className="btn btn-sm btn-toggle-following pull-right"><i className="fa fa-checkmark-round"></i> <span>Following</span></button>
                    </div>
                </div>
                <div className="media user-follower">
                    <img src="https://bootdey.com/img/Content/avatar/avatar2.png" alt="User Avatar"
                        className="media-object pull-left" />
                    <div className="media-body">
                        <a href="#">Michael<br /><span className="text-muted username">@iamichael</span></a>
                        <button type="button" className="btn btn-sm btn-default pull-right"><i className="fa fa-plus"></i> Follow
                    </button>
                    </div>
                </div>
            </div>
        </React.Fragment>
    );
}

export default FollowersComponent