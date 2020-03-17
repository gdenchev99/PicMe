import React from 'react';

function PostCommentsComponent(params) {

    return (
        <React.Fragment>
            <ul className="img-comment-list">
                <li>
                    <div className="comment-img">
                        <img src="http://lorempixel.com/50/50/people/6" />
                    </div>
                    <div className="comment-text">
                        <strong><a href="">Jane Doe</a></strong>
                        <p>Hello this is a test comment. Some extra text added.</p> <span className="date sub-text">on December 5th, 2016</span>
                    </div>
                </li>
            </ul>
        </React.Fragment>
    );
}

export default PostCommentsComponent