import React from 'react';
import { Trans } from 'react-i18next';
import {
  Collapse,
  DropdownItem,
  DropdownMenu,
  DropdownToggle,
  Nav,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
  UncontrolledDropdown
} from 'reactstrap';

import { connect } from 'react-redux';
import { NavLink as Link } from 'react-router-dom';
import { Action, Dispatch } from 'redux';
import { IOnlineServer, IUser } from 'src/models';
import { authActions, currentUserActions, onlineServerActions, } from '../store/actions';

interface IMainMenuProps {
  onLoad: () => void,
  logout: () => void,

  isAdmin: boolean,

  servers: IOnlineServer[]
}

interface IMainMenuState {
  isOpen: boolean
}

class MainMenu extends React.Component<IMainMenuProps, IMainMenuState> {
  constructor(props: IMainMenuProps) {
    super(props);

    this.toggle = this.toggle.bind(this);
    this.logoutClick = this.logoutClick.bind(this);

    this.state = {
      isOpen: false
    };
  }

  public componentDidMount() {
    const { onLoad } = this.props;
    onLoad();
  }

  public logoutClick(e: { preventDefault: () => void; }) {
    e.preventDefault();
    const { logout } = this.props;
    logout();
  }

  public toggle() {
    this.setState({
      isOpen: !this.state.isOpen
    });
  }
  public render() {

    const { servers, isAdmin } = this.props;

    return (
      <div>
        <Navbar color="dark" dark={true} expand="md">
          <NavbarBrand tag={Link} to="/" >BattlEye Manager</NavbarBrand>
          <NavbarToggler onClick={this.toggle} />
          <Collapse isOpen={this.state.isOpen} navbar={true}>
            <Nav className="mr-auto" navbar={true}>
              <UncontrolledDropdown nav={true} inNavbar={true}>
                <DropdownToggle nav={true} caret={true}>
                  <Trans>Servers</Trans>
                </DropdownToggle>
                <DropdownMenu right={true}>
                  {servers && servers.map((server) => <OnlineServer key={server.id} server={server} />)}
                  {!servers && <Trans>No active servers</Trans>}
                </DropdownMenu>
              </UncontrolledDropdown>
              {isAdmin && <AdminMenu />}
            </Nav>
            <Nav className="ml-auto" navbar={true}>
              <NavItem>
                <NavLink href="#" onClick={this.logoutClick}><Trans>Sign out</Trans></NavLink>
              </NavItem>
            </Nav>
          </Collapse>
        </Navbar>
      </div>
    );
  }
}

const AdminMenu = () => (
  <React.Fragment>
    <UncontrolledDropdown nav={true} inNavbar={true}>
      <DropdownToggle nav={true} caret={true}>
        <Trans>Admin</Trans>
      </DropdownToggle>
      <DropdownMenu right={true}>
        <DropdownItem tag={Link} to="/users">
          <Trans>Users</Trans>
        </DropdownItem>
        <DropdownItem tag={Link} to="/servers">
          <Trans>Servers</Trans>
        </DropdownItem>
      </DropdownMenu>
    </UncontrolledDropdown>
    <UncontrolledDropdown nav={true} inNavbar={true}>
      <DropdownToggle nav={true} caret={true}>
        <Trans>Dictionaries</Trans>
      </DropdownToggle>
      <DropdownMenu right={true}>
        <DropdownItem tag={Link} to="/kickReasons">
          <Trans>Kick reasons</Trans>
        </DropdownItem>
        <DropdownItem tag={Link} to="/banReasons">
          <Trans>Ban reasons</Trans>
        </DropdownItem>
      </DropdownMenu>
    </UncontrolledDropdown>
  </React.Fragment>
);


const OnlineServer = ({ server }: { server: IOnlineServer }) =>
  (
    <DropdownItem tag={Link} to={'/online/' + server.id}>
      {server.name}
    </DropdownItem>
  );

function mapStateToProps({ onlineServers: { items }, currentUser: { item } }: { onlineServers: { items: IOnlineServer[] }, currentUser: { item: IUser } }) {
  const isAdmin = item && item.isAdmin;
  return {
    servers: items,
    // tslint:disable-next-line: object-literal-sort-keys
    isAdmin
  };
}

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>) => {
  return {
    logout: () => dispatch(authActions.logout()),
    onLoad: () => {
      onlineServerActions.getItems()(dispatch);
      currentUserActions.getItem()(dispatch);
    }
  }
}

const connectedMainMenu = connect(mapStateToProps, mapDispatchToProps)(MainMenu);
export { connectedMainMenu as MainMenu }; 
