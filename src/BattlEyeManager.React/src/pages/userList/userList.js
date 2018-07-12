import React, { Component } from 'react';
import { userService } from '../../services';


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
        return (<div>
            {data.map((item, i) => (<div key={item.id}>{item.userName}</div>))}
        </div>
        );
    }
}