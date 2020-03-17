import React from 'react';

function LikeComponent(params) {

    return (
        <div className="cardbox-base">
            <ul>
                <li><a><i className="fas fa-heart"></i></a></li>
                <li><a href="#"><img src="http://www.themashabrand.com/templates/bootsnipp/post/assets/img/users/3.jpeg" className="img-fluid rounded-circle" alt="User" /></a></li>
                <li><a><span>242 Likes</span></a></li>
            </ul>
        </div>
    );
}

export default LikeComponent