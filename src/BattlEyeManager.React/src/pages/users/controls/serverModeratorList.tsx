import i18next from 'i18next';
import React, { Component } from 'react';
import { Trans, withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import { Button, Input } from 'reactstrap';
import { ClientGrid, ClientGridColumn, ClientGridColumns } from 'src/controls';
import { IServerModeratorItem, IUser } from 'src/models';
import { history, serverModeratorService, userService } from 'src/services';
import { Error } from '../../../controls';

interface IServerModeratorListState {
    data?: IServerModeratorItem[],

    user?: IUser,
    error: any
}

interface IServerModeratorListProps {
    t: i18next.TFunction
}

// i18next.TFunction

class ServerModeratorList extends Component<IServerModeratorListProps, IServerModeratorListState> {
    constructor(props: any) {
        super(props);
        this.state = { data: undefined, error: undefined, user: undefined };
        this.saveCallback = this.saveCallback.bind(this);
    }

    public async saveCallback() {
        const { match: { params: { id } } } = (this.props as any);
        const { data } = this.state;
        serverModeratorService.updateItems(id, data).then(
            () => history.push("/users"),
            (error: any) => this.setState({ error }))
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
        const { t } = this.props;

        let header = t("Visible servers");

        if (user) {
            header = t("Visible servers for user") + ": " + user.userName;
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
                <Button color="primary" onClick={this.saveCallback}><Trans>Save</Trans></Button>
                {' '}
                <Button tag={Link} to="/users" color="secondary"><Trans>Cancel</Trans></Button>
            </React.Fragment>
        );
    }
}


const ServerModeratorListTranslated = withTranslation()(ServerModeratorList)
export { ServerModeratorListTranslated as ServerModeratorList };