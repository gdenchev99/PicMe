import React from 'react';

function PrivateMessageComponent(params) {

    return (
        <React.Fragment>
            <div className="row rounded-lg overflow-hidden shadow">
            
                <div className="col-12 px-0">
                <div className="bg-gray px-4 py-2 bg-light">
                    <p>
                    <img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg" alt="user" width="50" className="rounded-circle" />
                    <span className="h5 mb-0 px-3 py-1">User1</span>
                    </p>
                </div>
                    <div className="px-4 py-5 chat-box chat bg-white">
                        <div className="media w-50 mb-3">
                            <img src="https://res.cloudinary.com/mhmd/image/upload/v1564960395/avatar_usae7z.svg"
                             alt="user" width="50" className="rounded-circle" />
                            <div className="media-body ml-3">
                                <div className="bg-light rounded py-2 px-3 mb-2">
                                    <p className="text-small mb-0 text-muted">Test which is a new approach all solutions</p>
                                </div>
                                <p className="small text-muted">12:00 PM | Aug 13</p>
                            </div>
                        </div>

                        <div className="media w-50 ml-auto mb-3">
                            <div className="media-body">
                                <div className="bg-primary rounded py-2 px-3 mb-2">
                                    <p className="text-small mb-0 text-white">Test which is a new approach to have all solutions</p>
                                </div>
                                <p className="small text-muted">12:00 PM | Aug 13</p>
                            </div>
                        </div>
                    </div>

                    <form action="#" className="bg-light">
                        <div className="input-group">
                            <textarea type="text" placeholder="Type a message" aria-describedby="button-addon2" className=" chat-input rounded-0 border-0 bg-light" />
                            <div className="input-group-append">
                                <button id="button-addon2" type="submit" className="btn btn-link"> <i className="fa fa-paper-plane"></i></button>
                            </div>
                        </div>
                    </form>

                </div>
            </div>
        </React.Fragment >
    );

}

export default PrivateMessageComponent