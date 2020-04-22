import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { LoginMenu } from './api-authorization/LoginMenu';
import './NavMenu.css';
import authService from './api-authorization/AuthorizeService';
import { Search } from './api-profiles/Search';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  componentDidMount() {
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
      <header>
        <Navbar className="navbar-expand ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand className="brand" tag={Link} to="/">PicMe</NavbarBrand>
            <NavItem className="d-sm-inline-flex flex-sm-row-reverse">
              <ul className="navbar-nav flex-grow">
                <LoginMenu>
                </LoginMenu>
              </ul>
            </NavItem>
          </Container>
        </Navbar>
      </header>
    );
  }
}
