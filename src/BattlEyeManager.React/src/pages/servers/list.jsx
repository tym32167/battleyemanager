import React, { Component } from 'react';
import { Table } from 'reactstrap';
import { Link } from 'react-router-dom';
import { serverActions } from "../../store/actions";
import { connect } from 'react-redux';
import { Button } from "reactstrap";
import {Error} from '../../controls';

class List extends Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.onLoad();
    }    

    render() {

        const { items, error } = this.props;
        const len = items.length;

        return (
            <React.Fragment>
                <h2>Servers ({len})</h2>
                <Error error={error} />
                <Button tag={Link} to="/servers/create" color="primary">Create</Button>
                {items && <Serverstable items={items} />}
            </React.Fragment>
        );
    }
}

const Serverstable = ({ items }) =>
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
            {items.map((item, i) => <ServerItem key={item.id} item={item} />)}
        </tbody>
    </Table>;

const ServerItem = ({ item }) => (
    <tr>
        <td>{item.lastName}</td>
        <td>{item.firstName}</td>
        <td>{item.userName}</td>
        <td>{item.email}</td>
        <td>
            <Button color="success" to={'/servers/' + item.id} tag={Link} size="sm">Edit</Button>
        </td>
        <td>
            
        </td>
    </tr>)


const mapStateToProps = ({ servers }) => {
    return {
        items: servers.items || [],
        error: servers.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: () => dispatch(serverActions.getAll())
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };