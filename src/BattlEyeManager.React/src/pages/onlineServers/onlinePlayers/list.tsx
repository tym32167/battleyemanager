import * as SignalR from '@aspnet/signalr';
import React from 'react';
import { connect } from 'react-redux';
import { ClientGrid, ClientGridColumn, ClientGridColumns, Error } from '../../../controls';
import { onlinePlayerActions } from "../../../store/actions";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Trans } from 'react-i18next';
import { Action, Dispatch } from 'redux';
import { IOnlinePlayer } from 'src/models';
import { BanPlayer } from './banPlayer';
import { KickPlayer } from './kickPlayer';

interface IListProps {
    serverId: number,
    onLoad: (serverId: number) => void,
    onKick: (serverId: number, player: IOnlinePlayer, kickReason: string) => void,
    onBan: (serverId: number, player: IOnlinePlayer, reason: string, minutes: number) => void,
    items: IOnlinePlayer[],
    busy: boolean,
    error: any
}

class List extends React.Component<IListProps> {

    private connection: SignalR.HubConnection;

    constructor(props: IListProps) {
        super(props);
        this.refresh = this.refresh.bind(this);
        this.kickPlayerCallback = this.kickPlayerCallback.bind(this);
        this.banPlayerCallback = this.banPlayerCallback.bind(this);
    }

    public kickPlayerCallback(player: IOnlinePlayer, { kickReason }: { kickReason: string }) {
        const { serverId, onKick } = this.props;
        onKick(serverId, player, kickReason);
    }

    public banPlayerCallback(player: IOnlinePlayer, { reason, minutes }: { reason: string, minutes: number }) {
        const { serverId, onBan } = this.props;
        onBan(serverId, player, reason, minutes);
    }

    public componentDidUpdate(prevProps: IListProps) {
        if (Number(prevProps.serverId) !== Number(this.props.serverId)) {
            this.refresh();
        }
    }

    public signalrStart() {

        this.connection = new SignalR.HubConnectionBuilder()
            .withUrl("/api/serverfallback")
            .build();

        this.connection.on('event', (id, message) => {
            const { serverId } = this.props;
            if (Number(id) === Number(serverId) && message === 'player') {
                this.refresh();
            }
        });

        this.connection.start()
            .catch(error => Promise.reject(error));
    }

    public signalrStop() {
        const connection = this.connection;
        if (connection && connection.stop) {
            connection.stop()
                .catch(error => Promise.reject(error));
        }
    }

    public componentWillUnmount() {
        this.signalrStop();
    }

    public componentDidMount() {
        this.refresh();
        this.signalrStart();
    }

    public refresh() {
        const { serverId } = this.props;
        this.props.onLoad(serverId);
    }

    public render() {

        const { items, error, busy } = this.props;
        const len = items.length;

        const kickRender = (row: IOnlinePlayer) => <KickPlayer player={row} onKick={this.kickPlayerCallback} />;
        const banRender = (row: IOnlinePlayer) => <BanPlayer player={row} onBan={this.banPlayerCallback} />;

        return (
            <React.Fragment>
                <h4><small><FontAwesomeIcon icon="sync" {...{ onClick: this.refresh }} /></small><Trans>Players</Trans> ({len}) {busy && <small>loading....</small>}</h4>
                <Error error={error} />
                {items &&
                    <React.Fragment>
                        <ClientGrid data={items} error={error} enableSort={true} enableFilter={true}
                            sortProps={{ sortField: "name", sortDirection: true }}
                            enableColumnManager={true}>
                            <ClientGridColumns>
                                <ClientGridColumn header="Num" name="num" />
                                <ClientGridColumn header="Name" name="name" />
                                <ClientGridColumn header="IP" name="ip" hidable={true} visible={true} />
                                <ClientGridColumn header="Port" name="port" hidable={true} visible={false} />
                                <ClientGridColumn header="Ping" name="ping" hidable={true} visible={false} />
                                <ClientGridColumn header="" name="" headerStyle={{ width: '1%' }} renderer={kickRender} />
                                <ClientGridColumn header="" name="" headerStyle={{ width: '1%' }} renderer={banRender} />
                            </ClientGridColumns>
                        </ClientGrid>
                    </React.Fragment>}
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ onlinePlayers }: { onlinePlayers: any }, ownProps: any) => {
    const server = onlinePlayers[ownProps.match.params.serverId];
    let items = [];
    if (server &&
        server.items) { items = server.items; }

    let error;
    if (server &&
        server.error) { error = server.error; }

    return {
        busy: server && server.busy,
        error,
        items,
        serverId: ownProps.match.params.serverId,
    }
}

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>) => {
    return {
        onLoad: (serverId: number) => {
            onlinePlayerActions.getItems(serverId)(dispatch);
        },

        onKick: (serverId: number, player: IOnlinePlayer, kickReason: string) => {
            onlinePlayerActions.kickPlayer(serverId, player, kickReason)(dispatch)
        },

        onBan: (serverId: number, player: IOnlinePlayer, reason: string, minutes: number) => {
            onlinePlayerActions.banPlayerOnline(serverId, minutes, player, reason)(dispatch)
        },
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };
