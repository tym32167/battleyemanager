import * as SignalR from '@aspnet/signalr';
import React from 'react';
import { connect } from 'react-redux';
// import { Table } from 'reactstrap';
import { BootstrapTable, Error, FilterControl, IBootstrapTableColumn, IFilterControlProps, ISortControlProps, SortControl } from '../../../controls';
import { onlinePlayerActions } from "../../../store/actions";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
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

        const columns: Array<IBootstrapTableColumn<IOnlinePlayer>> = [
            { header: "Num", name: "num" },
            { header: "Name", name: "name" },
            { header: "IP", name: "ip" },
            { header: "Port", name: "port" },
            { header: "Ping", name: "ping" },
            { header: "", renderer: row => (<KickPlayer player={row} onKick={this.kickPlayerCallback} />), headerStyle: { width: '1%' } },
            { header: "", renderer: row => (<BanPlayer player={row} onBan={this.banPlayerCallback} />), headerStyle: { width: '1%' } },
        ];

        const sortProps: ISortControlProps<IOnlinePlayer> = {
            children: (props2) => {
                const filterProps: IFilterControlProps<IOnlinePlayer> = {
                    ...props2,
                    children: (props) => <BootstrapTable columns={columns} {...props} />,
                };

                return (
                    <FilterControl {...filterProps} />
                );
            },
            data: items,
            sortDirection: true,
            sortField: "name"
        };

        return (
            <React.Fragment>
                <h4> <small><FontAwesomeIcon icon="sync" {...{ onClick: this.refresh }} /></small> Players ({len}) {busy && <small>loading....</small>}</h4>
                <Error error={error} />
                {items &&
                    <React.Fragment>
                        <SortControl {...sortProps} />
                    </React.Fragment>
                }
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
