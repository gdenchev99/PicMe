import React from 'react';
import { Link } from "react-router-dom";

function FeedCommentsComponent(params) {
    return (
        <div className="view-comments">
            {params.count <= 2 ? null :
                <Link to={`/post/${params.postId}`}><label>View all {params.count} comments</label></Link>}
            <ul className="feed-comments">
                {params.data.length <= 0 ? <div>No comments yet.</div> : params.data.map(c => <li key={c.id}>
                    <p>
                        <Link to={`/user/${c.creatorUserName}`}><span className="comments-username">{c.creatorUserName}</span></Link>
                        <span className="comment-text"> {c.text}</span>
                    </p>
                </li>)}
            </ul>

        </div>);
}

export default FeedCommentsComponent