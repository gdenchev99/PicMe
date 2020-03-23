import React, { Fragment } from 'react';
import 'emoji-mart/css/emoji-mart.css';
import { Picker } from 'emoji-mart';

function CreateCommentComponent(params) {

    return (
        <Fragment>
            <div id="emoji-picker" className="emoji-picker"><Picker onSelect={params.addEmoji} /></div>
            <div className="cardbox-comments">
                <div className="search">
                    <textarea className="comment-area" placeholder="Write a comment" name="text" type="text" value={params.state.text} onChange={params.handleChange} />
                    <i className="far fa-smile comment-emoji">
                        <button onClick={params.showPicker} className="btn btn-emoji"></button></i>
                    {params.state.text.replace(/\s/g, '').length ?
                        <button onClick={params.handleData} className="comment-btn btn"><p color="blue">Send</p></button> :
                        <button className="comment-btn btn" disabled>Send</button>}
                </div>
            </div>
        </Fragment>
    );
}

export default CreateCommentComponent