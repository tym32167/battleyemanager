import React from 'react';
import PropTypes from 'prop-types';
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
import { authActions, onlineServerActions } from '../store/actions';
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

  componentDidMount() {
    const { loadServers } = this.props;
    loadServers();
  }

  logoutClick(e) {
    e.preventDefault();
    const { logout } = this.props;
    logout();
  }

  toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  render() {

    const {servers} = this.props;

    return (
      <div>
        <Navbar color="dark" dark expand="md">
          <NavbarBrand tag={Link} to="/" >BattlEye Manager</NavbarBrand>
          <NavbarToggler onClick={this.toggle} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav className="mr-auto" navbar>             
              <NavItem>
                <NavLink tag={Link} to="/users" >Users</NavLink>
              </NavItem>
              <UncontrolledDropdown nav inNavbar>
                <DropdownToggle nav caret>
                  Servers
                </DropdownToggle>
                <DropdownMenu right>
                  <DropdownItem tag={Link} to="/servers">
                    Server list
                  </DropdownItem>
                  <DropdownItem divider />
                  
                  {servers && servers.map((server) => <OnlineServer key={server.id} server={server} /> )}

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

MainMenu.propTypes = {
  logout: PropTypes.func,
  loadServers: PropTypes.func,
  servers : PropTypes.array
}

const OnlineServer = ({ server }) =>
  (
    <DropdownItem  tag={Link} to={'/online/' + server.id + '/players'}>
      {server.name}
    </DropdownItem>
  );

OnlineServer.propTypes = {
  server: PropTypes.object.isRequired
}

function mapStateToProps(state) {
  return {
    servers: state.onlineServers.items
  };
}

const mapDispatchToProps = (dispatch) => {
  return {
    logout: () => dispatch(authActions.logout()),
    loadServers: () => dispatch(onlineServerActions.getItems())
  }
}

const connectedMainMenu = connect(mapStateToProps, mapDispatchToProps)(MainMenu);
export { connectedMainMenu as MainMenu }; 
