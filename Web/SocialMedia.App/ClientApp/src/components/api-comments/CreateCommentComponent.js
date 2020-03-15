import React from 'react';

function CreateCommentComponent(params) {

    return (
        <form onSubmit={params.handleData}>
            <div className="cardbox-comments">
                <div className="search">
                    <textarea className="comment-area" placeholder="Write a comment" name="text" type="text" value={params.state.text} onChange={params.handleChange}/>
                    {params.state.text.length > 0 ? 
                    <button className="comment-btn btn"><p color="blue">Send</p></button> : 
                    <button className="comment-btn btn" disabled>Send</button>}
                </div>
            </div>
        </form>
    );
}

export default CreateCommentComponent