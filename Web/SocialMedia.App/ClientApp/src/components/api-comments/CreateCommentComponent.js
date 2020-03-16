import React from 'react';

function CreateCommentComponent(params) {

    return (
        <div className="cardbox-comments">
            <form onSubmit={params.handleData}>
                <div className="search">
                    <textarea className="comment-area" placeholder="Write a comment" name="text" type="text" value={params.state.text} onChange={params.handleChange} />
                    {params.state.text.length > 0 ?
                        <button className="comment-btn btn"><p color="blue">Send</p></button> :
                        <button className="comment-btn btn" disabled>Send</button>}
                </div>
            </form>
        </div>
    );
}

export default CreateCommentComponent