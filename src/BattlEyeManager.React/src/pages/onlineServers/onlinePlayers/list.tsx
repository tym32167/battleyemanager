import * as SignalR from '@aspnet/signalr';
import React from 'react';
import { connect } from 'react-redux';
// import { Table } from 'reactstrap';
import { Error } from '../../../controls';
import { onlinePlayerActions } from "../../../store/actions";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Dispatch } from 'redux';
import { IOnlinePlayer } from 'src/models';
import { KickPlayer } from './kickPlayer';

import ReactTable, { Column, DefaultFilterFunction } from 'react-table';
import "react-table/react-table.css";


interface IListProps {
    serverId: number,
    onLoad: (serverId: number) => void,
    items: IOnlinePlayer[],
    busy: boolean,
    error: any
}

class List extends React.Component<IListProps> {

    private connection: SignalR.HubConnection;

    // constructor(props) {
    //     super(props);
    //     this.refresh = this.refresh.bind(this);
    // }

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

        const columns: Array<Column<IOnlinePlayer>> = [
            { Header: 'Num', accessor: 'num', },
            { Header: 'Name', accessor: 'name', },
            { Header: 'IP', accessor: 'ip', },
            { Header: 'Port', accessor: 'port', },
            { Header: 'Ping', accessor: 'ping', },
            {
                Cell: ({ row }: { row: IOnlinePlayer }) => {
                    // tslint:disable-next-line:no-console
                    // console.log(row);
                    // return (<div />)
                    return (<KickPlayer player={row} />)
                },
                Header: '',
                filterable: false,
            },
        ];

        const defaultFilterMethod: DefaultFilterFunction = (filter, row) =>
            String(row[filter.id]).toLowerCase().includes(String(filter.value).toLowerCase());

        return (
            <React.Fragment>
                <h4> <small><FontAwesomeIcon icon="sync" {...p} /></small> Players ({len}) {busy && <small>loading....</small>}</h4>
                <Error error={error} />
                {/* {items && <ItemsTable items={items} />} */}


                {items && <ReactTable data={items} columns={columns} filterable={true}
                    defaultFilterMethod={defaultFilterMethod}
                />}
            </React.Fragment>
        );
    }
}

// const ItemsTable = ({ items }: { items: IOnlinePlayer[] }) =>
//     <Table size="sm" responsive={true}>
//         <thead>
//             <tr>
//                 <th>Num</th>
//                 <th>Name</th>
//                 <th>IP</th>
//                 <th>Port</th>
//                 <th>Ping</th>
//                 <th />
//             </tr>
//         </thead>
//         <tbody>
//             {items && items.map && items.map((item, i) => <Item key={i} item={item} />)}
//         </tbody>
//     </Table>;

// const Item = ({ item }: { item: IOnlinePlayer }) => (
//     <tr>
//         <td>{item.num}</td>
//         <td>{item.name}</td>
//         <td>{item.ip}</td>
//         <td>{item.port}</td>
//         <td>{item.ping}</td>
//         <td>
//             <KickPlayer player={item} />
//         </td>
//     </tr>)


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

// List.propTypes = {
//     onLoad: PropTypes.func.isRequired,
//     items: PropTypes.array,
//     serverId: PropTypes.oneOfType([PropTypes.number, PropTypes.string]),
//     error: PropTypes.oneOfType([PropTypes.string, PropTypes.object]),
//     busy: PropTypes.bool
// }

// ItemsTable.propTypes = {
//     items: PropTypes.array
// }

// Item.propTypes = {
//     item: PropTypes.object
// }