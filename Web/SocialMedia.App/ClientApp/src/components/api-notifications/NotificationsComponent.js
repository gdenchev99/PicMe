import React from 'react';
import { Link } from 'react-router-dom';
import InfiniteScroll from 'react-infinite-scroll-component';

function NotificationsComponent(params) {
    return (
        <React.Fragment>
            <div className="card col-6 offset-3">
                <div className="card-title">Requests</div>
                <div className="card-body">
                    <Link to={`/user/${params.username}/requests`}><p>View all follower requests.</p></Link>
                </div>
            </div>
        <InfiniteScroll dataLength={params.data.length} next={params.loadMore} hasMore={true}>
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
        </InfiniteScroll>
        </React.Fragment >
    );
}

export default NotificationsComponent