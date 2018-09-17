import PropTypes from 'prop-types';
import React from 'react';
import { connect } from 'react-redux';

const ServerHeader = ({ title }) => (
    <h4 className="p-0 m-0">{title}</h4>
);

ServerHeader.propTypes = {
    title: PropTypes.string
}

const mapStateToProps = ({ onlineServers: { items } }, { match: { params: { serverId } } }) => {
    const server = items && items.find(item => Number(item.id) === Number(serverId));
    return {
        title: server && server.name
    }
}

const ConnectedServerHeader = connect(
    mapStateToProps
)(ServerHeader);

export { ConnectedServerHeader as ServerHeader };