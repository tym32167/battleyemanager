import React, { Component } from 'react';
import { onlineChatActions } from "../../../store/actions";
import { connect } from 'react-redux';
import { Error, Chat } from '../../../controls';
import PropTypes from 'prop-types';
import * as SignalR from '@aspnet/signalr';
import { Trans } from 'react-i18next';


import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

class List extends Component {

    constructor(props) {
        super(props);
        this.refresh = this.refresh.bind(this);
    }

    componentDidUpdate(prevProps) {
        if (Number(prevProps.serverId) !== Number(this.props.serverId)) {
            this.refresh();
        }
    }

    signalrStart() {

        this.connection = new SignalR.HubConnectionBuilder()
            .withUrl("/api/serverfallback")
            .build();

        this.connection.on('event', (id, message) => {
            const { serverId } = this.props;
            if (Number(id) === Number(serverId) && message === 'chat') {
                this.refresh();
            }
        });

        this.connection.start()
            .catch(error => Promise.reject(error));
    }

    signalrStop() {
        const connection = this.connection;
        if (connection && connection.stop) {
            connection.stop()
                .catch(error => Promise.reject(error));
        }
    }

    componentWillUnmount() {
        this.signalrStop();
    }

    componentDidMount() {
        this.refresh();
        this.signalrStart();
    }

    refresh() {
        const { serverId } = this.props;
        this.props.onLoad(serverId);
    }

    render() {

        const { items, error, busy, newMessage } = this.props;
        const len = items.length;

        return (
            <React.Fragment>
                <h4> <small><FontAwesomeIcon icon="sync" onClick={(e) => this.refresh()} /></small> <Trans>Chat</Trans> ({len}) {busy && <small>loading....</small>} </h4>
                <Error error={error} />
                {items && <Chat items={items} newMessage={newMessage} />}
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ onlineChat }, ownProps) => {

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
        error: error,
        busy: server && server.busy
    }
}

const mapDispatchToProps = (dispatch, { match: { params: { serverId } } }) => {
    return {
        onLoad: (serverId) => {
            dispatch(onlineChatActions.getItems(serverId));
        },
        newMessage: (value) => {
            dispatch(onlineChatActions.addItem(serverId, { audience: -1, message: value.message }));
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

