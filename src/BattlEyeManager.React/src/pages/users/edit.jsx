import React, { Component } from 'react';
import { userService } from '../../services';
import { Table } from 'reactstrap';
import { Link } from 'react-router-dom';

export class Edit extends Component {

    constructor(props) {
        super(props);
        this.state = {
            id: props.match.params.id
        };
        this.fetchData = this.fetchData.bind(this);
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData() {
        var id = this.state.id;
        userService.getUser(id)
            .then(data => {
                this.setState({ user: data });
            });
    }

    render() {        
        return (
            <div className="my-3 p-3 bg-white rounded box-shadow">
                <h2>Edit User</h2>                
            </div>
        );
    }
}