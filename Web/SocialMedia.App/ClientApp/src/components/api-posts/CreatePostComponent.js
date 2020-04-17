import React from 'react';
import ReactPlayer from 'react-player';
import { InlineError } from '../errors/InlineErrors';

function CreatePostComponent(params) {

    return (
        <form onSubmit={params.handleData}>
            <div className="card gedf-card">
                <div className="card-header">
                    <ul className="nav nav-tabs card-header-tabs" id="myTab" role="tablist">
                        <li className="nav-item">
                            <a className="nav-link active" id="posts-tab" data-toggle="tab" href="#posts" role="tab" aria-controls="posts" aria-selected="true">Description</a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link" id="images-tab" data-toggle="tab" role="tab" aria-controls="images" aria-selected="false" href="#images">Media</a>
                        </li>
                    </ul>
                </div>
                <div className="card-body">
                    <div className="tab-content" id="myTabContent">
                        <div className="tab-pane fade show active" id="posts" role="tabpanel" aria-labelledby="posts-tab">
                            <div className="form-group">
                                <label className="sr-only" htmlFor="message">post</label>
                                <textarea className="form-control" name="description" id="message"
                                    value={params.state.description}
                                    onChange={params.handleChange} rows="3" placeholder="What are you thinking?"></textarea>
                            </div>

                        </div>
                        <div className="tab-pane fade" id="images" role="tabpanel" aria-labelledby="images-tab">
                            <div className="form-group">
                                <div className="custom-file">
                                    <input type="file" accept="video/*, image/*" className="custom-file-input" id="customFile"
                                        onChange={params.handleMedia} />
                                    <label className="custom-file-label" htmlFor="customFile">{params.state.fileName}</label>
                                </div>
                                <InlineError field='MediaSource' errors={params.state.errors} />
                                {params.state.loading ?
                                    <div className="spinner-border spinner float-right" role="status">
                                        <span className="sr-only spinner float-right">Uploading...</span>
                                    </div> : null}
                                <h3>Media preview: </h3>
                                {
                                    params.state.media == null ? null :
                                        params.state.fileName.substr(params.state.fileName.lastIndexOf('.')) === ".mp4" ?
                                            <ReactPlayer url={params.state.media} playing={false}
                                                controls wrapper="form-group" className="preview-media" /> :
                                            <img src={params.state.media} className="preview-media" alt="preview-media" />
                                }
                            </div>
                            <div className="py-4"></div>
                        </div>
                    </div>
                    <div className="btn-toolbar justify-content-between">
                        <div className="btn-group">
                            {params.state.loading ?
                                <button type="submit" className="btn btn-primary" disabled>Posting...</button> :
                                <button type="submit" className="btn btn-primary">Post</button>}
                        </div>
                    </div>
                </div>
            </div>
        </form>
    );
}
export default CreatePostComponent