import React, { Component } from 'react';
import { Table, Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { onlineServerActions } from "../../store/actions";
import { connect } from 'react-redux';
import { Error } from '../../controls';
import PropTypes from 'prop-types';

class List extends Component {

    componentDidMount() {
        this.props.onLoad();
    }

    render() {

        const { items, error } = this.props;
        const len = items.length;

        return (
            <React.Fragment>
                <h2>Online Servers ({len})</h2>
                <Error error={error} />
                {items && <Serverstable items={items} />}
            </React.Fragment>
        );
    }
}

const Serverstable = ({ items }) =>
    <Table size="sm" responsive>
        <thead>
            <tr>
                <th>Name</th>
                <th style={{ width: "1%" }} >Host</th>
                <th style={{ width: "1%" }}>Port</th>
                <th style={{ width: "1%" }}>Active</th>

                <th style={{ width: "1%" }}>Connected</th>
                <th style={{ width: "1%" }}>Players</th>
                <th style={{ width: "1%" }}>Bans</th>
                <th style={{ width: "1%" }}>Admins</th>
                <th style={{ width: "1%" }}></th>
            </tr>
        </thead>
        <tbody>
            {items.map((item, i) => <ServerItem key={item.id} item={item} />)}
        </tbody>
    </Table>;

const ServerItem = ({ item }) => (
    <tr>
        <td style={{ whiteSpace: "nowrap" }} >{item.name}</td>
        <td>{item.host}</td>
        <td>{item.port}</td>
        <td>
            <input type="checkbox" disabled checked={item.active} />
        </td>

        <td>
            <input type="checkbox" disabled checked={item.isConnected} />
        </td>

        <td>{item.playersCount}</td>
        <td>{item.bansCount}</td>
        <td>{item.adminsCount}</td>

        <td>
            <Button color="success" to={'/online/' + item.id} tag={Link} size="sm">Dashboard</Button>
        </td>
    </tr>)


const mapStateToProps = ({ onlineServers }) => {
    return {
        items: onlineServers.items || [],
        error: onlineServers.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: () => dispatch(onlineServerActions.getItems())
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };

List.propTypes = {
    onLoad: PropTypes.func.isRequired,
    items: PropTypes.array,
    error: PropTypes.oneOfType([PropTypes.string, PropTypes.object])
}

Serverstable.propTypes = {
    items: PropTypes.array
}

ServerItem.propTypes = {
    item: PropTypes.object
}