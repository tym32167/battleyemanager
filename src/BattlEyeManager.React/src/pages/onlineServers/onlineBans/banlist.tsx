import * as SignalR from '@aspnet/signalr';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { IOnlineBan, IOnlineServer } from 'src/models';
import { onlineBanActions } from 'src/store/actions';
import { BootstrapTable, Error, FilterControl, IBootstrapTableColumn, IFilterControlProps, IPagerControlProps, ISortControlProps, PagerControl, SortControl } from '../../../controls';
import { ServerHeader } from '../onlineServerHeader';
import { RemoveBan } from './removeBan';

export const BanList = (props: any) => (
    <React.Fragment>
        <div className="container-fluid p-lg-3 p-1">
            <div className="row">
                <div className="col-12 p-2 m-0" >
                    <div className="bg-white rounded box-shadow  p-1">
                        <ServerHeader {...props} />
                    </div>
                </div>

                <div className="col-sm-12 col-lg-11  p-2 m-0">
                    <div className="bg-white rounded box-shadow p-1">
                        <ConnectedBanListTable {...props} />
                    </div>
                </div>
            </div>
        </div>
    </React.Fragment>
);

interface IBanListProps {
    onLoad: (serverId: number) => void,
    items: IOnlineBan[],
    serverId: number,
    error: any
}



class BanListTable extends Component<IBanListProps> {
    public t: IBanListProps;
    private connection: SignalR.HubConnection;
    constructor(props: IBanListProps) {
        super(props);
        this.refresh = this.refresh.bind(this);
    }

    public componentDidUpdate(prevProps: IBanListProps) {
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
            if (Number(id) === Number(serverId) && message === 'banlist') {
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

        const { items, error } = this.props;
        const len = items ? items.length : 0;

        const columns: Array<IBootstrapTableColumn<IOnlineBan>> = [
            { header: "Num", name: "num" },
            { header: "Minutes left", name: "minutesleft" },
            { header: "Reason", name: "reason" },
            { header: "Guid or IP", name: "guidIp" },
            { header: "", renderer: (ban) => <RemoveBan ban={ban} /> },
        ];

        const sortProps: ISortControlProps<IOnlineBan> = {
            children: (props2) => {
                const filterProps: IFilterControlProps<IOnlineBan> = {
                    ...props2,
                    children: (props) => {
                        const pagerProps: IPagerControlProps<IOnlineServer> = {
                            ...props,
                            children: (p) => <BootstrapTable columns={columns} {...p} />,
                            pageSize: 50,
                        }
                        return (<PagerControl {...pagerProps} />);
                    }
                };

                return (
                    <FilterControl {...filterProps} />
                );
            },
            data: items,
        };

        return (
            <React.Fragment>
                <h2><small><FontAwesomeIcon icon="sync" {...{ onClick: this.refresh }} /></small> Online Bans ({len})</h2>
                <Error error={error} />
                {items &&
                    <React.Fragment>
                        <SortControl {...sortProps} />
                    </React.Fragment>}
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ onlineBans }: { onlineBans: any }, ownProps: any) => {
    const server = onlineBans[ownProps.match.params.serverId];
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
            dispatch(onlineBanActions.getItems(serverId));
        }
    }
}

const ConnectedBanListTable = connect(
    mapStateToProps,
    mapDispatchToProps
)(BanListTable);
