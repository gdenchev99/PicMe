import React from 'react';
import { Link } from 'react-router-dom';

function PostCommentsComponent(params) {

    return (
        <React.Fragment>
            {params.data.length <= 0 ? null : params.data.map(c => <li key={c.id}>
                <div className="comment-img">
                    <img src={c.creatorProfilePictureUrl} />
                </div>
                <div className="comment-text">
                    <strong><Link to={"/user/" + c.creatorUserName}>
                        {c.creatorUserName}</Link></strong>
                    <p>{c.text}
                        {params.currentUser === c.creatorUserName || params.isPostCreator ?
                        <button onClick={() => params.handleDelete(c.id)} className="btn">
                            <i className="fas fa-times"></i></button> : null}
                    </p> 
                    <span className="date sub-text">{c.createdOnFormat}</span>
                </div>
            </li>)}
            <center><button onClick={params.loadMore} className="btn">Load More</button></center>
        </React.Fragment>
    );
}

export default PostCommentsComponent