import React from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem
} from 'reactstrap';

import { NavLink as Link } from 'react-router-dom';
import { userActions } from '../actions';
import { connect } from 'react-redux';


class MainMenu extends React.Component {
  constructor(props) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.logoutClick = this.logoutClick.bind(this);

    this.state = {
      isOpen: false
    };
  }

  logoutClick(e) {
    e.preventDefault();
    const { dispatch } = this.props;
    dispatch(userActions.logout());
  }

  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  render() {
    return (      
        <div>
          <Navbar color="dark" dark expand="md">
            <NavbarBrand tag={Link} to="/" >BattlEye Manager</NavbarBrand>
            <NavbarToggler onClick={this.toggle} />
            <Collapse isOpen={this.state.isOpen} navbar>
              <Nav className="mr-auto" navbar>
                <NavItem>
                  <NavLink tag={Link} to="/test" >Test</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} to="/users" >Users</NavLink>
                </NavItem>
                <UncontrolledDropdown nav inNavbar>
                  <DropdownToggle nav caret>
                    Servers
                </DropdownToggle>
                  <DropdownMenu right>
                    <DropdownItem>
                      Server 1
                  </DropdownItem>
                    <DropdownItem>
                      Server 2
                  </DropdownItem>
                    <DropdownItem divider />
                    <DropdownItem>
                      Add server
                  </DropdownItem>
                  </DropdownMenu>
                </UncontrolledDropdown>
              </Nav>
              <Nav className="ml-auto" navbar>
                <NavItem>
                  <NavLink href="#" onClick={this.logoutClick}>Sign out</NavLink>
                </NavItem>                
              </Nav>
            </Collapse>
          </Navbar>
        </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    state
  };
}

const connectedMainMenu = connect(mapStateToProps)(MainMenu);
export { connectedMainMenu as MainMenu }; 
