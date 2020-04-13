import React from 'react';
import { Link } from 'react-router-dom';

function NotificationsComponent(params) {
    return (
        <React.Fragment>
            {params.data.map(n =>
                <div key={n.id} className="card col-6 offset-3">
                    <div className="card-body">
                        <Link to={`/user/${n.info.match(params.regex)[0]}`}>
                            <span>{n.info}</span>
                        </Link>
                        <Link to={`/post/${n.postId}`}>
                            <img src={n.postMediaSource} style={{ height: "50px", position: "absolute", bottom: "7px", right: 0 }} />
                        </Link>
                    </div>
                </div>)}

        </React.Fragment>
    );
}

export default NotificationsComponent