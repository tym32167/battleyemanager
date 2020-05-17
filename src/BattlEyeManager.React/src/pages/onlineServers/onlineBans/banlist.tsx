import * as SignalR from '@aspnet/signalr';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { Component } from 'react';
import { Trans } from 'react-i18next';
import { connect } from 'react-redux';
import { Container, Row } from 'reactstrap';
import { Action, Dispatch } from 'redux';
import { IOnlineBan } from 'src/models';
import { onlineBanActions } from 'src/store/actions';
import { ClientGrid, ClientGridColumn, ClientGridColumns, Error } from '../../../controls';
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

        const removeBanRenderer = (ban: IOnlineBan) => <RemoveBan ban={ban} />

        return (
            <React.Fragment>
                <Container fluid={true}>
                    <Row>
                        <h2><small><FontAwesomeIcon icon="sync" {...{ onClick: this.refresh }} /></small> <Trans>Online Bans</Trans> ({len})</h2>
                    </Row>
                    <Row>
                        <Error error={error} />
                    </Row>
                    <Row>
                        {items &&
                            <React.Fragment>
                                <ClientGrid data={items} error={error} enableSort={true} enablePager={true} enableFilter={true}
                                    sortProps={{ sortField: "num", sortDirection: false }}
                                    enableColumnManager={true}
                                    pageSize={50}>
                                    <ClientGridColumns>
                                        <ClientGridColumn header="Num" name="num" visible={true} hidable={true} />
                                        <ClientGridColumn header="Name" name="playerName" visible={true} hidable={true} />
                                        <ClientGridColumn header="Comment" name="playerComment" visible={false} hidable={true} />
                                        <ClientGridColumn header="Minutes left" name="minutesleft" visible={true} hidable={true} />
                                        <ClientGridColumn header="Reason" name="reason" visible={true} hidable={true} />
                                        <ClientGridColumn header="Guid or IP" name="guidIp" visible={false} hidable={true} />
                                        <ClientGridColumn header="" name="" renderer={removeBanRenderer} />
                                    </ClientGridColumns>
                                </ClientGrid>
                            </React.Fragment>}
                    </Row>
                </Container>
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

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>) => {
    return {
        onLoad: (serverId: number) => {
            onlineBanActions.getItems(serverId)(dispatch);
        }
    }
}

const ConnectedBanListTable = connect(
    mapStateToProps,
    mapDispatchToProps
)(BanListTable);
