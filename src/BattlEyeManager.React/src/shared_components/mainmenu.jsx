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
import { authActions, onlineServerActions, currentUserActions } from '../store/actions';
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
    const { onLoad } = this.props;
    onLoad();
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

    const { servers, isAdmin } = this.props;

    return (
      <div>
        <Navbar color="dark" dark expand="md">
          <NavbarBrand tag={Link} to="/" >BattlEye Manager</NavbarBrand>
          <NavbarToggler onClick={this.toggle} />
          <Collapse isOpen={this.state.isOpen} navbar>
            <Nav className="mr-auto" navbar>
              <UncontrolledDropdown nav inNavbar>
                <DropdownToggle nav caret>
                  Servers
                </DropdownToggle>
                <DropdownMenu right>
                  {servers && servers.map((server) => <OnlineServer key={server.id} server={server} />)}
                </DropdownMenu>
              </UncontrolledDropdown>
              {isAdmin && <AdminMenu />}
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
  onLoad: PropTypes.func,
  servers: PropTypes.array,
  isAdmin: PropTypes.bool
}

const AdminMenu = () => (
  <UncontrolledDropdown nav inNavbar>
    <DropdownToggle nav caret>
      Admin
                </DropdownToggle>
    <DropdownMenu right>
      <DropdownItem tag={Link} to="/users">
        Users
                  </DropdownItem>
      <DropdownItem tag={Link} to="/servers">
        Servers
                  </DropdownItem>
    </DropdownMenu>
  </UncontrolledDropdown>
);


const OnlineServer = ({ server }) =>
  (
    <DropdownItem tag={Link} to={'/online/' + server.id}>
      {server.name}
    </DropdownItem>
  );

OnlineServer.propTypes = {
  server: PropTypes.object.isRequired
}

function mapStateToProps({ onlineServers: { items }, currentUser: { item } }) {
  const isAdmin = item && item.isAdmin;
  return {
    servers: items,
    isAdmin: isAdmin
  };
}

const mapDispatchToProps = (dispatch) => {
  return {
    logout: () => dispatch(authActions.logout()),
    onLoad: () => {
      dispatch(onlineServerActions.getItems());
      dispatch(currentUserActions.getItem());
    }
  }
}

const connectedMainMenu = connect(mapStateToProps, mapDispatchToProps)(MainMenu);
export { connectedMainMenu as MainMenu }; 
