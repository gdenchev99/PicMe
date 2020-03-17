import React from 'react';

function PostCommentsComponent(params) {

    return (
        <React.Fragment>
            {params.data.length <= 0 ? null : params.data.map(c => <li key={c.customId}>
                    <div className="comment-img">
                        <img src={c.creatorProfilePictureUrl} />
                    </div>
                    <div className="comment-text">
                        <strong><a href="">{c.creatorUserName}</a></strong>
                        <p>{c.text}</p> <span className="date sub-text">{c.createdOnFormat}</span>
                    </div>
                </li> )}
        </React.Fragment>
    );
}

export default PostCommentsComponent