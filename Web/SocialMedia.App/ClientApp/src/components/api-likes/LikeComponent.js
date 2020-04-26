import React from 'react';
import { Link } from "react-router-dom";

function LikeComponent(params) {
    return (
        <div className="cardbox-base">
            <ul>
                <li><button onClick={params.handleClick} className="like">
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 48 48"><path className="like-icon" fill={params.state.svgFill} clipRule="evenodd" d={params.state.svgPath} fillRule="evenodd" /></svg>
                </button></li>
                {params.state.pictures.map(p => <li key={p.id}><Link to={"/user/" + p.userUserName}><img src={p.userProfilePictureUrl} className="img-fluid rounded-circle" alt="User" /></Link></li>)}
                <li><a><span>{params.state.likesCount}
                    {params.state.likesCount <= 1 && params.state.likesCount !== 0 ? " Like" : " Likes"}</span></a></li>
            </ul>
        </div>
    );
}

export default LikeComponent