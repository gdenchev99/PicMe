import React, { Component } from 'react';
import { Link } from "react-router-dom";

export class Footer extends Component {

    render() {
        return (
            <footer className="footer-extra font-small bg-white">
                <div className="footer-copyright text-center text-dark py-1">© 2020 Copyright:
                    <Link to="/" className="text-dark"> Social Media</Link>
                </div>
            </footer>
        );
    }
}