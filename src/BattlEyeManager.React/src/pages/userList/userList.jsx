import React, { Component } from 'react';
import { userService } from '../../services';
import { Table } from 'reactstrap';


export class UserList extends Component {

    constructor(props) {
        super(props);
        this.state = {
            items: []
        };
        this.fetchData = this.fetchData.bind(this);
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData() {
        userService.getUsers()
            .then(data => {
                this.setState({ items: data });
            });
    }

    render() {
        const data = this.state.items || [];
        return (
            <React.Fragment>                
                <div className="my-3 p-3 bg-white rounded box-shadow">
                    <h1>Users</h1>
                    <Userstable items={data} />
                </div>
            </React.Fragment>
        );
    }
}

const Userstable = ({ items }) => <Table size="sm">
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
        {items.map((item, i) => <UserItem key={item.id} item={item} />)}
    </tbody>
</Table>;

const UserItem = ({ item }) => (
    <tr>
        <td>{item.lastName}</td>
        <td>{item.firstName}</td>
        <td>{item.userName}</td>
        <td>{item.email}</td>
        <th></th>
    </tr>)
