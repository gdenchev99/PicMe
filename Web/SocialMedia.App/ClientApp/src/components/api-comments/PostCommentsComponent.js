import React from 'react';
import { Link } from 'react-router-dom';

function PostCommentsComponent(params) {

    return (
        <React.Fragment>
            {params.data.length <= 0 ? null : params.data.map(c => <li key={c.customId}>
                <div className="comment-img">
                    <img src={c.creatorProfilePictureUrl} />
                </div>
                <div className="comment-text">
                    <strong><Link to={"/user/" + c.creatorUserName}>
                        {c.creatorUserName}</Link></strong>
                    <p>{c.text} <button className="btn"><i class="fas fa-times"></i></button></p> 
                    <span className="date sub-text">{c.createdOnFormat}</span>
                </div>
            </li>)}
        </React.Fragment>
    );
}

export default PostCommentsComponent