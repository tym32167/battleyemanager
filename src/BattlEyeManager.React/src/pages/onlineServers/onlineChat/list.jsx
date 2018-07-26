import React, { Component } from 'react';
import { Table } from 'reactstrap';
import { Link } from 'react-router-dom';
import { onlineChatActions, onlineServerActions } from "../../../store/actions";
import { connect } from 'react-redux';
import { Error } from '../../../controls';
import PropTypes from 'prop-types';

class List extends Component {

    componentDidMount() {
        const { serverId } = this.props;
        this.props.onLoad(serverId);
    }

    render() {

        const { items, error, server, serverId } = this.props;
        const len = items.length;

        return (
            <React.Fragment>
                <h2>{server && server.name}</h2>
                <h3><Link to={'/online/' + serverId + '/players'}>Players</Link> {' '} Chat ({len})</h3>
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
                <th>Message</th>
            </tr>
        </thead>
        <tbody>
            {items.map((item, i) => <Item key={i} item={item} />)}
        </tbody>
    </Table>;

const Item = ({ item }) => (
    <tr>        
        <td>{item.message}</td>
    </tr>)


const mapStateToProps = ({ onlineChat, onlineServers }, ownProps) => {
    const server = onlineChat[ownProps.match.params.serverId];
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
        server: onlineServers.item,
        error: error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (serverId) => {
            dispatch(onlineChatActions.getItems(serverId));
            dispatch(onlineServerActions.getItem(serverId));
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
    server: PropTypes.object,
    serverId: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
    error: PropTypes.oneOfType([PropTypes.string, PropTypes.object])
}

ItemsTable.propTypes = {
    items: PropTypes.array
}

Item.propTypes = {
    item: PropTypes.object
}