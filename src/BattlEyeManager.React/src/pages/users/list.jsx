import React, { Component } from 'react';
import { Table } from 'reactstrap';
import { Link } from 'react-router-dom';
import { userActions } from "../../store/actions";
import { connect } from 'react-redux';

class List extends Component {

    componentDidMount() {
        this.props.onLoad();
    }

    render() {

        const { users } = this.props;
        const len = users.length;

        console.log(users);

        return (
            <React.Fragment>
                <h2>Users ({len})</h2>
                {users && <Userstable users={users} />}
            </React.Fragment>
        );
    }
}

const Userstable = ({ users }) => <Table size="sm">
    <thead>
        <tr>
            <th>Last Name</th>
            <th>First Name</th>
            <th>User Name</th>
            <th>Email</th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        {users.map((user, i) => <UserItem key={user.id} user={user} />)}
    </tbody>
</Table>;

const UserItem = ({ user }) => (
    <tr>
        <td>{user.lastName}</td>
        <td>{user.firstName}</td>
        <td>{user.userName}</td>
        <td>{user.email}</td>
        <th><Link to={'/users/' + user.id} className="btn-sm btn-success">Edit</Link></th>
    </tr>)


const mapStateToProps = ({ users }) => {
    return {
        users: users.users || []
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: () => dispatch(userActions.getUsers())
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };