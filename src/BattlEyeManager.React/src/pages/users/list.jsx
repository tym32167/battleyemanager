import React, {Component} from 'react';
import {Table} from 'reactstrap';
import {Link} from 'react-router-dom';
import {userActions} from "../../actions";
import {connect} from 'react-redux';

class List extends Component {

    constructor({users, onLoad}) {
        super();
        this.state = {
            items: users,
            onLoad
        };
    }

    componentDidMount() {
        this.state.onLoad();
    }

    render() {
        const data = this.state.items || [];
        return (
            <div className="my-3 p-3 bg-white rounded box-shadow">
                <h2>Users</h2>
                <Userstable items={data}/>
            </div>
        );
    }
}

const Userstable = ({items}) => <Table size="sm">
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
    {items.map((item, i) => <UserItem key={item.id} item={item}/>)}
    </tbody>
</Table>;

const UserItem = ({item}) => (
    <tr>
        <td>{item.lastName}</td>
        <td>{item.firstName}</td>
        <td>{item.userName}</td>
        <td>{item.email}</td>
        <th><Link to={'/users/' + item.id}>Edit</Link></th>
    </tr>)


const mapStateToProps = ({user}) => {
    console.log(user);
    return {
        users: user.users
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

export {ConnectedList as List};