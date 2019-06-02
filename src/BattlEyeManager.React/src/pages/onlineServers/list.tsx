import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';
import { ClientGrid, ClientGridColumn, IGridParentProps } from 'src/controls';
import { IOnlineServer } from 'src/models';
import { onlineServerService } from 'src/services';

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
        const dashRender = (row: IOnlineServer) => (<Button color="success" to={'/online/' + row.id} tag={Link} size="sm">Dashboard</Button>);

        return (
            <React.Fragment>
                <ClientGrid data={data} error={error} header="Online Servers" enableSort={true}>
                    <ClientGridColumn header="Name" name="name" rowStyle={{ whiteSpace: "nowrap" }} />
                    <ClientGridColumn header="Host" name="host" headerStyle={{ width: "1%" }} />
                    <ClientGridColumn header="Port" name="port" headerStyle={{ width: "1%" }} />
                    <ClientGridColumn header="Active" name="active" headerStyle={{ width: "1%" }} />
                    <ClientGridColumn header="Connected" name="isConnected" headerStyle={{ width: "1%" }} />
                    <ClientGridColumn header="Players" name="playersCount" headerStyle={{ width: "1%" }} />
                    <ClientGridColumn header="Bans" name="bansCount" headerStyle={{ width: "1%" }} />
                    <ClientGridColumn header="Admins" name="adminsCount" headerStyle={{ width: "1%" }} />
                    <ClientGridColumn header="" name="" headerStyle={{ width: "1%" }} renderer={dashRender} />
                </ClientGrid>
            </React.Fragment>
        );
    }
}