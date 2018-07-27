import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { onlineChatActions, onlineServerActions } from "../../../store/actions";
import { connect } from 'react-redux';
import { Error, Chat } from '../../../controls';
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
                <h3>{server && server.name}</h3>
                <h4><Link to={'/online/' + serverId + '/players'}>Players</Link> {' '} Chat ({len})</h4>
                <Error error={error} />
                {items && <Chat items={items} />}
            </React.Fragment>
        );
    }
}

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

