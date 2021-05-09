import moment from 'moment';
import React, { Component } from 'react';
import { ClientGrid, ClientGridColumn, ClientGridColumns } from 'src/controls';
import { IPagedResponse } from 'src/models';
import { IPlayerSession } from 'src/models/iplayersession';
import { onlinePlayersService, onlineServerService } from 'src/services';
import { ServerHeader } from '../onlineServerHeader';
import { BanPlayerOffline } from './banPlayeroffline';


export const PlayerSessionList = (props: any) => (
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
                        <List {...props} />
                    </div>
                </div>
            </div>
        </div>
    </React.Fragment>
);


interface IListProps {
    serverId: number,
    // onBan: (serverId: number, player: IPlayerSession, reason: string, minutes: number) => void,
    data?: IPlayerSession[],
    busy?: boolean,
    error?: any
}

class List extends Component<any, IListProps> {
    constructor(props: any) {
        super(props);
        this.state = { serverId: props.match.params.serverId };

        this.banPlayerCallback = this.banPlayerCallback.bind(this);
    }

    public componentDidMount() {
        this.Load();
    }

    public Load() {
        const { serverId } = this.state;
        return onlineServerService.getPlayerSessions(serverId, 0, 5000).then(
            (response: IPagedResponse<IPlayerSession>) =>
                this.setState({ data: response.data, error: undefined }),
            (error: any) => this.setState({ data: [], error }));
    }

    public banPlayerCallback(playerId: number, { reason, minutes }: { reason: string, minutes: number }) {
        const { serverId } = this.state;
        onlinePlayersService.BanPlayerOffline(serverId, minutes, reason, playerId);
    }

    public render() {
        const { data, error } = this.state || {};
        const startDateRender = (row: IPlayerSession) => this.renderDate(row.startDate);
        const endDateRender = (row: IPlayerSession) => row.endDate && this.renderDate(row.endDate);

        const banOfflineRender = (row: IPlayerSession) => <BanPlayerOffline playerId={row.playerId} playerName={row.name} onBan={this.banPlayerCallback} />

        return (
            <React.Fragment>
                <ClientGrid data={data} error={error} header="Sessions" enablePager={true} enableFilter={true} >
                    <ClientGridColumns>
                        <ClientGridColumn header="Name" name="name" />
                        <ClientGridColumn header="IP" name="ip" />
                        <ClientGridColumn header="Start Date" name="startDate" renderer={startDateRender} />
                        <ClientGridColumn header="End Date" name="endDate" renderer={endDateRender} />
                        <ClientGridColumn header="" name="" renderer={banOfflineRender} />
                    </ClientGridColumns>
                </ClientGrid>
            </React.Fragment>
        );
    }
    private renderDate(date: Date) {
        return moment.utc(date).local().format('YYYY-MM-DD HH:mm:ss');
    }
}
