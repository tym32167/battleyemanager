import React from 'react';
import { Trans } from 'react-i18next';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { IOnlineServer } from 'src/models';

interface IServerHeaderProps {
    id: number | undefined,
    title: string | undefined
}

const ServerHeader = ({ id, title }: IServerHeaderProps) => (
    <React.Fragment>
        <h4 className="p-0 m-0">{title}</h4>
        <Link to={'/online/' + id}> <Trans>Dashboard</Trans></Link>
        {' '}
        <Link to={'/online/' + id + '/sessions'}><Trans>Sessions</Trans></Link>
        {' '}
        <Link to={'/online/' + id + '/bans'}><Trans>Bans</Trans></Link>
        {' '}
        <Link to={'/online/' + id + '/manage'}><Trans>Manage</Trans></Link>
        {' '}
        <Link to={'/online/' + id + '/steam'}><Trans>Steam</Trans></Link>
        {' '}
        <Link to={'/online/' + id + '/options'}><Trans>Options</Trans></Link>
    </React.Fragment>
);

const mapStateToProps = ({ onlineServers: { items } }: { onlineServers: { items: IOnlineServer[] } },
    { match: { params: { serverId } } }: { match: { params: { serverId: number } } }) => {
    const server = items && items.find(item => Number(item.id) === Number(serverId));
    return {
        id: server && server.id,
        title: server && server.name,
    }
}

const ConnectedServerHeader = connect(
    mapStateToProps
)(ServerHeader);

export { ConnectedServerHeader as ServerHeader };