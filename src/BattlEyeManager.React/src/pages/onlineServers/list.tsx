import React, { Component } from 'react';
import { Trans } from 'react-i18next';
import { Link } from 'react-router-dom';
import { Button, Container, Row } from 'reactstrap';
import { ClientGrid, ClientGridColumn, ClientGridColumns, IGridParentProps } from 'src/controls';
import { IOnlineServer } from 'src/models';
import { onlineServerService, serverStatsService } from 'src/services';
import { ServerStats } from './serverStats/serverStats';

export class List extends Component<any, IGridParentProps<IOnlineServer>> {
    constructor(props: any) {
        super(props);
        this.state = {};
    }

    public componentDidMount() {
        this.Load();
    }

    public Load() {
        return onlineServerService.getItems().then(
            (items: IOnlineServer[]) => this.setState({ data: items, error: undefined }),
            (error: any) => this.setState({ data: undefined, error }));
    }
    public render() {
        const { data, error } = this.state;
        const dashRender = (row: IOnlineServer) => (<Button color="success" to={'/online/' + row.id} tag={Link} size="sm"><Trans>Dashboard</Trans></Button>);

        return (
            <React.Fragment>
                <Container>
                    <Row>
                        <ClientGrid data={data} error={error} header="Online Servers" enableSort={true} enableColumnManager={true}>
                            <ClientGridColumns>
                                <ClientGridColumn header="Name" name="name" rowStyle={{ whiteSpace: "nowrap" }} />
                                <ClientGridColumn header="Host" name="host" headerStyle={{ width: "1%" }} hidable={true} visible={false} />
                                <ClientGridColumn header="Port" name="port" headerStyle={{ width: "1%" }} hidable={true} visible={false} />
                                <ClientGridColumn header="Active" name="active" headerStyle={{ width: "1%" }} hidable={true} visible={false} />
                                <ClientGridColumn header="Connected" name="isConnected" headerStyle={{ width: "1%" }} />
                                <ClientGridColumn header="Players" name="playersCount" headerStyle={{ width: "1%" }} />
                                <ClientGridColumn header="Bans" name="bansCount" headerStyle={{ width: "1%" }} />
                                <ClientGridColumn header="Admins" name="adminsCount" headerStyle={{ width: "1%" }} />
                                <ClientGridColumn header="" name="" headerStyle={{ width: "1%" }} renderer={dashRender} />
                            </ClientGridColumns>
                        </ClientGrid>
                    </Row>
                    <Row>
                        <ServerStats loader={serverStatsService.getStatsLastDay} header={"Servers last 24 hours"} />
                    </Row>
                    <Row>
                        <ServerStats loader={serverStatsService.getStatsLastWeek} header={"Servers last 7 days"} />
                    </Row>
                </Container>
            </React.Fragment>
        );
    }
}