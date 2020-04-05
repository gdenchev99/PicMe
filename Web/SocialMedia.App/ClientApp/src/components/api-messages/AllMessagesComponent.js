import React from 'react';
import { Link } from 'react-router-dom';

function AllMessagesComponent(params) {
    return (
        <div className="col-12 px-0">
            <div className="bg-white">

                <div className="bg-gray px-4 py-2 bg-light">
                    <p className="h5 mb-0 py-1">Recent</p>
                </div>

                <div className="messages-box chat">
                    <div className="list-group rounded-0">
                        {params.data.map(c =>
                            <Link key={c.id} to={`/messages/u/${c.username}`}
                            className="list-group-item list-group-item-action text-white rounded-0">
                                <div className="media">
                                    <img src={c.profilePicture} alt="user" width="50" className="rounded-circle" />
                                    <div className="media-body ml-4">
                                        <div className="d-flex align-items-center justify-content-between mb-1 text-dark">
                                            <h6 className="mb-0">{c.recipientFullName}</h6>
                                            <small className="small font-weight-bold">{c.lastMessageDate}</small>
                                        </div>
                                        <p className="font-italic mb-0 text-small">{c.lastMessage}</p>
                                    </div>
                                </div>
                            </Link> )}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default AllMessagesComponent