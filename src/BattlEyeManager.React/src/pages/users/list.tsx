import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';
import { ClientGrid, ClientGridColumn, ClientGridColumns, IGridParentProps } from 'src/controls';
import { IUser } from 'src/models';
import { userService } from 'src/services';

export class List extends Component<any, IGridParentProps<IUser>> {
    constructor(props: any) {
        super(props);
        this.state = {};
        this.deleteCallback = this.deleteCallback.bind(this);
    }

    public componentDidMount() {
        this.Load();
    }

    public Load() {
        return userService.getItems().then(
            (items: IUser[]) => this.setState({ data: items, error: undefined }),
            (error: any) => this.setState({ data: undefined, error }));
    }

    public async deleteCallback(user: IUser) {
        if (window.confirm('Are you sure you want to delete user ' + user.userName + '?')) {
            await userService.deleteItem(user.id);
            this.Load();
        }
    }

    public render() {
        const { data, error } = this.state;

        const editRender = (user: IUser) => <Button color="success" to={'/users/' + user.id} tag={Link} size="sm">Edit</Button>;
        const editVisibleRender = (user: IUser) => <Button color="success" to={'/users/visibleServers/' + user.id} tag={Link} size="sm">Edit Servers</Button>;
        const deleteRender = (user: IUser) => <Button color="danger" size="sm" onClick={this.deleteCallback.bind(this, user)}>Delete</Button>;

        const header = () => <Button tag={Link} to="/users/create" color="primary">Create</Button>;

        return (
            <React.Fragment>
                <ClientGrid data={data} error={error} header="Users" beforeGrid={header} enableSort={true}>
                    <ClientGridColumns>
                        <ClientGridColumn header="Last Name" name="lastName" />
                        <ClientGridColumn header="First Name" name="firstName" />
                        <ClientGridColumn header="User Name" name="userName" />
                        <ClientGridColumn header="Email" name="email" />
                        <ClientGridColumn header="Is Admin" name="isAdmin" />
                        <ClientGridColumn header="" name="" renderer={editRender} />
                        <ClientGridColumn header="" name="" renderer={editVisibleRender} />
                        <ClientGridColumn header="" name="" renderer={deleteRender} />
                    </ClientGridColumns>
                </ClientGrid>
            </React.Fragment>
        );
    }
}
