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
                        <Link to='/messages/u/user1' className="list-group-item list-group-item-action active text-white rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-1">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">25 Dec</small>
                                    </div>
                                    <p className="font-italic mb-0 text-small">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.</p>
                                </div>
                            </div>
                        </Link>

                        <a href="#" className="list-group-item list-group-item-action list-group-item-light rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-1">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">14 Dec</small>
                                    </div>
                                    <p className="font-italic text-muted mb-0 text-small">Lorem ipsum dolor sit amet, consectetur. incididunt ut labore.</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" className="list-group-item list-group-item-action list-group-item-light rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-1">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">9 Nov</small>
                                    </div>
                                    <p className="font-italic text-muted mb-0 text-small">consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" className="list-group-item list-group-item-action list-group-item-light rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-1">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">18 Oct</small>
                                    </div>
                                    <p className="font-italic text-muted mb-0 text-small">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" className="list-group-item list-group-item-action list-group-item-light rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-1">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">17 Oct</small>
                                    </div>
                                    <p className="font-italic text-muted mb-0 text-small">consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" className="list-group-item list-group-item-action list-group-item-light rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-1">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">2 Sep</small>
                                    </div>
                                    <p className="font-italic text-muted mb-0 text-small">Quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" className="list-group-item list-group-item-action list-group-item-light rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-1">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">30 Aug</small>
                                    </div>
                                    <p className="font-italic text-muted mb-0 text-small">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.</p>
                                </div>
                            </div>
                        </a>

                        <a href="#" className="list-group-item list-group-item-action list-group-item-light rounded-0">
                            <div className="media"><img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-4">
                                    <div className="d-flex align-items-center justify-content-between mb-3">
                                        <h6 className="mb-0">Jason Doe</h6><small className="small font-weight-bold">21 Aug</small>
                                    </div>
                                    <p className="font-italic text-muted mb-0 text-small">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.</p>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default AllMessagesComponent