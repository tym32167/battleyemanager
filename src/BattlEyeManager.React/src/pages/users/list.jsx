import React, { Component } from 'react';
import { Table } from 'reactstrap';
import { Link } from 'react-router-dom';
import { userActions } from "../../store/actions";
import { connect } from 'react-redux';
import { Button } from "reactstrap";
import {Error} from '../../controls';
import PropTypes from 'prop-types';

class List extends Component {

    constructor(props) {
        super(props);

        this.deleteCallback = this.deleteCallback.bind(this);
    }

    componentDidMount() {
        this.props.onLoad();
    }

    deleteCallback(user) {
        if (window.confirm('Are you sure you want to delete user ' + user.username + '?')) {
            const { deleteUser } = this.props;
            deleteUser(user);
        }
    }

    render() {

        const { users, error } = this.props;
        const len = users.length;

        return (
            <React.Fragment>
                <h2>Users ({len})</h2>
                <Error error={error} />
                <Button tag={Link} to="/users/create" color="primary">Create</Button>
                {users && <Userstable users={users} deleteUser={this.deleteCallback} />}
            </React.Fragment>
        );
    }
}

const Userstable = ({ users, deleteUser }) =>
    <Table size="sm">
        <thead>
            <tr>
                <th>Last Name</th>
                <th>First Name</th>
                <th>User Name</th>
                <th>Email</th>
                <th colSpan="2" className="table-fit"></th>
            </tr>
        </thead>
        <tbody>
            {users.map((user, i) => <UserItem key={user.id} user={user} deleteUser={deleteUser} />)}
        </tbody>
    </Table>;

const UserItem = ({ user, deleteUser }) => (
    <tr>
        <td>{user.lastName}</td>
        <td>{user.firstName}</td>
        <td>{user.userName}</td>
        <td>{user.email}</td>
        <td>
            <Button color="success" to={'/users/' + user.id} tag={Link} size="sm">Edit</Button>
        </td>
        <td>
            <Button color="danger" size="sm" onClick={(e) => deleteUser(user)}>Delete</Button>
        </td>
    </tr>)


const mapStateToProps = ({ users }) => {
    return {
        users: users.users || [],
        error: users.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: () => dispatch(userActions.getUsers()),
        deleteUser: (user) => dispatch(userActions.deleteUser(user))
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };


List.propTypes = {
    deleteUser: PropTypes.func.isRequired,
    onLoad: PropTypes.func.isRequired,
    users: PropTypes.object,
    error: PropTypes.object
}

Userstable.propTypes = {
    deleteUser: PropTypes.func.isRequired,    
    users: PropTypes.object
}

UserItem.propTypes = {
    deleteUser: PropTypes.func.isRequired,
    user: PropTypes.object
}