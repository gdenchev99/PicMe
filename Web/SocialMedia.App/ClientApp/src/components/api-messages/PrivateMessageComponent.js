import React from 'react';
import { Link } from 'react-router-dom';
import { Picker } from 'emoji-mart';

function PrivateMessageComponent(params) {

    return (
        <React.Fragment>
            <div className="emoji-picker chat-picker"><Picker onSelect={params.addEmoji} /></div>
            <div className="row rounded-lg overflow-hidden shadow">

                <div className="col-12 px-0">
                    <div className="bg-gray px-4 py-2 bg-light">
                        <p>
                            <img src={params.state.receiverPicture} alt="user" width="50" className="rounded-circle" />
                            <Link to={`/user/${params.state.receiverUsername}`}>
                                <span className="h5 mb-0 px-3 py-1">{params.state.receiverUsername}</span></Link>
                        </p>
                    </div>
                    <div ref={params.windowRef} className="px-4 py-5 chat-box chat bg-white">
                        {params.data.map(m => m.userUserName === params.state.myUsername ?
                            <div key={m.id} className="media w-50 ml-auto mb-3">
                                <div className="media-body">
                                    <div className="bg-primary rounded py-2 px-3 mb-2">
                                        <p className="text-small mb-0 text-white">{m.text}</p>
                                    </div>
                                    <p className="small text-muted">{m.createdOn}</p>
                                </div>
                            </div> :
                            <div key={m.id} className="media w-50 mb-3">
                                <img src={m.userProfilePictureUrl}
                                    alt="user" width="50" className="rounded-circle" />
                                <div className="media-body ml-3">
                                    <div className="bg-light rounded py-2 px-3 mb-2">
                                        <p className="text-small mb-0 text-muted">{m.text}</p>
                                    </div>
                                    <p className="small text-muted">{m.createdOn}</p>
                                </div>
                            </div>
                        )}
                    </div>

                    <div className="bg-light">
                        <div className="input-group">
                            <textarea type="text" placeholder="Type a message" name="text"
                                onChange={params.handleChange} value={params.state.text}
                                aria-describedby="button-addon2" className=" chat-input rounded-0 border-0 bg-light" />
                            <div className="input-group-append">
                            <i className="far fa-smile chat-emoji-icon comment-emoji">
                                    <button onClick={params.showPicker} className="btn btn-emoji"></button></i>
                            {params.state.text.replace(/\s/g, '').length ?
                                <button id="button-addon2" type="submit" onClick={params.sendMessage}
                                    className="btn btn-link"><i className="fa fa-paper-plane"></i></button> :
                                <button id="button-addon2" className="btn btn-link"><i className="fa fa-paper-plane"></i></button>}
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </React.Fragment >
    );

}

export default PrivateMessageComponent