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
            <Userstable items={data} />
        );
    }
}

const Userstable = ({ items }) => <Table size="sm">
    <thead>
        <tr>
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
        <td>{item.userName}</td>
        <td>{item.email}</td>
        <th></th>
    </tr>)