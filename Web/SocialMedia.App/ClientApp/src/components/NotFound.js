import React, { Component } from 'react';
import { Link } from 'react-router-dom';

export class NotFound extends Component {
    render() {
        return (
            <div className="error-404">
                <div className="error-code m-b-10 m-t-20">404
                <i className="fa fa-warning"></i>
                </div>
                <h2 className="font-bold">Oops 404! That page can’t be found.</h2>

                <div className="error-desc">
                    Sorry, but the page you are looking for was either not found or does not exist. <br />
                    Try refreshing the page or click the button below to go back to the Homepage.
                    <div><br />
                        <Link to="/"><span className="glyphicon glyphicon-home"></span> Go back to Homepage</Link>
                    </div>
                </div>
            </div>
        );
    }
}