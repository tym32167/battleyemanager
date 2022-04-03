import React, { Component } from 'react';
import { EditForm } from './controls';
import { Error } from '../../controls';
import { IServer } from 'src/models';
import { history, serverService } from 'src/services';
import { Trans } from 'react-i18next';

interface IServerEditState {
    item?: IServer,
    error?: any
}

export class Edit extends Component<any, IServerEditState> {

    constructor(props: any) {
        super(props);
        this.state = {};
    }

    public componentDidMount() {
        const { match: { params: { id } } } = (this.props as any);
        serverService.getItem(id).then(
            (item) => this.setState({ item }),
            (error) => this.setState({ error }));
    }

    public Update(item: IServer) {
        serverService.updateItem(item).then(
            () => history.push("/servers"),
            (error: any) => this.setState({ error }));
    }

    public render() {
        const { item, error } = this.state;

        const onSubmit = (i: IServer) => {
            this.Update(i)
        };

        return (
            <React.Fragment>
                <h2><Trans>Edit Server</Trans></h2>
                <Error error={error} />
                {item && <EditForm onSubmit={onSubmit} initialValues={item} {...{ edit: true }} />}
            </React.Fragment>
        );
    }
}



