import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { Table } from 'reactstrap';
import { Button, FormGroup, Input } from "reactstrap";
import { Dispatch } from 'redux';
import { IUser } from 'src/models';
import { Error } from '../../controls';
import { userActions } from "../../store/actions";

interface IListProps {
    users: IUser[],
    error: any,

    deleteUser: (user: IUser) => void,
    onLoad: () => void
}

class List extends Component<IListProps> {

    constructor(props: IListProps) {
        super(props);

        this.deleteCallback = this.deleteCallback.bind(this);
    }

    public componentDidMount() {
        this.props.onLoad();
    }

    public deleteCallback(user: IUser) {
        if (window.confirm('Are you sure you want to delete user ' + user.userName + '?')) {
            const { deleteUser } = this.props;
            deleteUser(user);
        }
    }

    public render() {

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

interface IUserstableProps {
    users: IUser[],
    deleteUser: (user: IUser) => void
}

const Userstable = ({ users, deleteUser }: IUserstableProps) =>
    <Table size="sm" responsive={true}>
        <thead>
            <tr>
                <th>Last Name</th>
                <th>First Name</th>
                <th>User Name</th>
                <th>Email</th>
                <th>Is Admin</th>
                <th colSpan={2} className="table-fit" />
            </tr>
        </thead>
        <tbody>
            {users.map((user: any, _: any) => <UserItem key={user.id} user={user} deleteUser={deleteUser} />)}
        </tbody>
    </Table>;

interface IUserItemProps {
    user: IUser,
    deleteUser: (user: IUser) => void
}

const UserItem = ({ user, deleteUser }: IUserItemProps) => {

    const click = (e: any) => deleteUser(user);

    return (
        <tr>
            <td>{user.lastName}</td>
            <td>{user.firstName}</td>
            <td>{user.userName}</td>
            <td>{user.email}</td>
            <td>
                <FormGroup check={true}>
                    <Input type="checkbox" checked={user.isAdmin} disabled={true} />
                </FormGroup>
            </td>
            <td>
                <Button color="success" to={'/users/' + user.id} tag={Link} size="sm">Edit</Button>
            </td>
            <td>
                <Button color="danger" size="sm" onClick={click}>Delete</Button>
            </td>
        </tr>);
}


const mapStateToProps = ({ users }: { users: any }) => {
    return {
        error: users.error,
        users: users.items || [],
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        deleteUser: (user: IUser) => dispatch(userActions.deleteItem(user)),
        onLoad: () => dispatch(userActions.getItems()),
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };

