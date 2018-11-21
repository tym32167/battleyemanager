import * as SignalR from '@aspnet/signalr';
import React from 'react';
import { connect } from 'react-redux';
// import { Table } from 'reactstrap';
import { BootstrapTable, Error, FilterControl, IBootstrapTableColumn, IFilterControlProps } from '../../../controls';
import { onlinePlayerActions } from "../../../store/actions";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Dispatch } from 'redux';
import { IOnlinePlayer } from 'src/models';
import { KickPlayer } from './kickPlayer';

interface IListProps {
    serverId: number,
    onLoad: (serverId: number) => void,
    items: IOnlinePlayer[],
    busy: boolean,
    error: any
}

class List extends React.Component<IListProps> {

    private connection: SignalR.HubConnection;

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
        const p = { refresh: this.refresh };

        const columns: Array<IBootstrapTableColumn<IOnlinePlayer>> = [
            { header: "Num", renderer: row => row.num },
            { header: "Name", renderer: row => row.name },
            { header: "IP", renderer: row => row.ip },
            { header: "Port", renderer: row => row.port },
            { header: "Ping", renderer: row => row.ping },
            { header: "", renderer: row => (<KickPlayer player={row} />) },
        ];

        const filterProps: IFilterControlProps<IOnlinePlayer> = {
            children: (props) => <BootstrapTable columns={columns} {...props} />,
            data: items
        }

        return (
            <React.Fragment>
                <h4> <small><FontAwesomeIcon icon="sync" {...p} /></small> Players ({len}) {busy && <small>loading....</small>}</h4>
                <Error error={error} />
                {items &&
                    <React.Fragment>
                        <FilterControl {...filterProps} />
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

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        onLoad: (serverId: number) => {
            dispatch(onlinePlayerActions.getItems(serverId));
        }
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(List);

export { ConnectedList as List };
