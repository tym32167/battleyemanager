import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Input } from 'reactstrap';
import { ClientGrid, ClientGridColumn, ClientGridColumns } from 'src/controls';
import { IServerModeratorItem, IUser } from 'src/models';
import { serverModeratorService, userService } from 'src/services';
import { Error } from '../../../controls';

interface IServerModeratorListState {
    data?: IServerModeratorItem[],

    user?: IUser,
    error: any
}

export class ServerModeratorList extends Component<any, IServerModeratorListState> {
    constructor(props: any) {
        super(props);
        this.state = { data: undefined, error: undefined, user: undefined };
        this.saveCallback = this.saveCallback.bind(this);
    }

    public saveCallback() {
        const { match: { params: { id } } } = (this.props as any);
        const { data } = this.state;
        serverModeratorService.updateItems(id, data);
    }

    public componentDidMount() {
        this.Load();
    }

    public async Load() {
        const { match: { params: { id } } } = (this.props as any);

        await serverModeratorService.getItems(id).then(
            (items: IServerModeratorItem[]) => {
                this.setState({ data: items, error: undefined });
            },
            (error: any) => this.setState({ data: undefined, error }));

        await userService.getItem(id).then(
            (item: IUser) => this.setState({ user: item, error: undefined, data: this.state.data }),
            (error: any) => this.setState({ error }));
    }

    public render() {
        const { user, data, error } = this.state;

        let header = "Visible servers";

        if (user) {
            header = "Visible servers for user: " + user.userName;
        }

        const print = (item: IServerModeratorItem) => {
            // tslint:disable-next-line: no-shadowed-variable
            // const { data } = this.state;
            item.isChecked = !item.isChecked;
            this.setState(this.state);
        }

        // tslint:disable-next-line: jsx-no-lambda
        const isCheckedRender = (item: IServerModeratorItem) => <Input type="checkbox" checked={item.isChecked} onChange={v => print(item)} />;

        return (
            <React.Fragment>
                <Error error={error} />
                <ClientGrid data={data} error={error} header={header} showLen={false} enableSort={true}>
                    <ClientGridColumns>
                        <ClientGridColumn header="Name" name="serverName" />
                        <ClientGridColumn header="Visible" name="isChecked" renderer={isCheckedRender} />
                    </ClientGridColumns>
                </ClientGrid>
                <Button color="primary" onClick={this.saveCallback} >Save</Button>
                {' '}
                <Button tag={Link} to="/users" color="secondary">Cancel</Button>
            </React.Fragment>
        );
    }
}
