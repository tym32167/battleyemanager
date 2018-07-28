import React, { Component } from 'react';
import { Table, Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { onlinePlayerActions } from "../../../store/actions";
import { connect } from 'react-redux';
import { Error } from '../../../controls';
import PropTypes from 'prop-types';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

class List extends Component {

    constructor(props) {
        super(props);
        this.refresh = this.refresh.bind(this);
    }

    componentDidMount() {
        this.refresh();
    }

    refresh() {
        const { serverId } = this.props;
        this.props.onLoad(serverId);
    }

    render() {

        const { items, error, busy } = this.props;
        const len = items.length;

        return (
            <React.Fragment>                
                <h4> <small><FontAwesomeIcon icon="sync" onClick={(e) => this.refresh()} /></small> Players ({len}) {busy && <small>loading....</small>}</h4>                
                <Error error={error} />
                {items && <ItemsTable items={items} />}
            </React.Fragment>
        );
    }
}

const ItemsTable = ({ items }) =>
    <Table size="sm">
        <thead>
            <tr>
                <th>Num</th>
                <th>Name</th>
                <th>IP</th>
                <th>Port</th>
                <th>Ping</th>
                <th></th>
            </tr>
        </thead>
        <tbody>
            {items.map((item, i) => <Item key={i} item={item} />)}
        </tbody>
    </Table>;

const Item = ({ item }) => (
    <tr>
        <td>{item.num}</td>
        <td>{item.name}</td>
        <td>{item.ip}</td>
        <td>{item.port}</td>
        <td>{item.ping}</td>        
        <td>
            <Button color="success" to={'/online/' + item.id + '/players'} tag={Link} size="sm">Players</Button>
        </td>
    </tr>)


const mapStateToProps = ({ onlinePlayers }, ownProps) => {
    const server = onlinePlayers[ownProps.match.params.serverId];
    let items = [];
    if (server &&
        server.items)
        items = server.items;

    let error = undefined;
    if (server &&
        server.error)
        error = server.error;

    return {
        serverId: ownProps.match.params.serverId,
        items: items,
        error: error,
        busy: onlinePlayers.busy
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (serverId) => {
            dispatch(onlinePlayerActions.getItems(serverId));
        }
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
    serverId: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
    error: PropTypes.oneOfType([PropTypes.string, PropTypes.object]),
    busy: PropTypes.bool
}

ItemsTable.propTypes = {
    items: PropTypes.array
}

Item.propTypes = {
    item: PropTypes.object
}