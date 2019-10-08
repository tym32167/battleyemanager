import React, { Component } from 'react';
import { Trans } from 'react-i18next';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';
import { ClientGrid, ClientGridColumn, ClientGridColumns, IGridParentProps } from 'src/controls';
import { IServer } from 'src/models';
import { serverService } from 'src/services';

export class List extends Component<any, IGridParentProps<IServer>> {
    constructor(props: any) {
        super(props);
        this.state = {};
        this.deleteCallback = this.deleteCallback.bind(this);
    }

    public componentDidMount() {
        this.Load();
    }

    public Load() {
        return serverService.getItems().then(
            (items: IServer[]) => this.setState({ data: items, error: undefined }),
            (error: any) => this.setState({ data: undefined, error }));
    }

    public async deleteCallback(item: IServer) {
        if (window.confirm('Are you sure you want to delete server ' + item.name + '?')) {
            await serverService.deleteItem(item.id);
            this.Load();
        }
    }
    public render() {
        const { data, error } = this.state;

        const editRender = (item: IServer) => <Button color="success" to={'/servers/' + item.id} tag={Link} size="sm"><Trans>Edit</Trans></Button>;
        const deleteRender = (item: IServer) => <Button color="danger" size="sm" onClick={this.deleteCallback.bind(this, item)}><Trans>Delete</Trans></Button>;

        const header = () => <Button tag={Link} to="/servers/create" color="primary"><Trans>Create</Trans></Button>;

        return (
            <React.Fragment>
                <ClientGrid data={data} error={error} header="Servers" beforeGrid={header} enableSort={true}>
                    <ClientGridColumns>
                        <ClientGridColumn header="Name" name="name" />
                        <ClientGridColumn header="Host" name="host" />
                        <ClientGridColumn header="Port" name="port" />
                        <ClientGridColumn header="Active" name="active" />
                        <ClientGridColumn header="Welcome feature" name="welcomeFeatureEnabled" />
                        <ClientGridColumn header="" name="" renderer={editRender} />
                        <ClientGridColumn header="" name="" renderer={deleteRender} />
                    </ClientGridColumns>
                </ClientGrid>
            </React.Fragment>
        );
    }
}
