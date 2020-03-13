import React, { Component } from 'react';

export class Footer extends Component {

    render() {
        return (
            <footer className="footer-extra font-small bg-white">
                <div className="footer-copyright text-center text-light py-1">© 2020 Copyright:
                    <a href="/" className="text-light"> Social Media</a>
                </div>
            </footer>
        );
    }
}