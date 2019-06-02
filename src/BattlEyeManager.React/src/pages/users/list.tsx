import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, FormGroup, Input } from 'reactstrap';
import { ClientGrid, ClientGridColumn } from 'src/controls';
import { IUser } from 'src/models';
import { userService } from 'src/services';


export class List extends Component {

    constructor(props: any) {
        super(props);
        this.deleteCallback = this.deleteCallback.bind(this);
    }

    public deleteCallback(user: IUser) {
        if (window.confirm('Are you sure you want to delete user ' + user.userName + '?')) {
            userService.deleteItem(user.id);
        }
    }
    public render() {
        const fetch = () => userService.getItems();

        const isAdminRender = (user: IUser) => (
            <FormGroup check={true}>
                <Input type="checkbox" checked={user.isAdmin} disabled={true} />
            </FormGroup>);

        const editRender = (user: IUser) => <Button color="success" to={'/users/' + user.id} tag={Link} size="sm">Edit</Button>;
        const deleteRender = (user: IUser) => <Button color="danger" size="sm" onClick={this.deleteCallback.bind(this, user)}>Delete</Button>;

        const header = () => <Button tag={Link} to="/users/create" color="primary">Create</Button>;

        return (
            <React.Fragment>
                <ClientGrid fetch={fetch} header="Users" beforeGrid={header} enableSort={true}>
                    <ClientGridColumn header="Last Name" name="lastName" />
                    <ClientGridColumn header="First Name" name="firstName" />
                    <ClientGridColumn header="User Name" name="userName" />
                    <ClientGridColumn header="Email" name="email" />
                    <ClientGridColumn header="Is Admin" name="" renderer={isAdminRender} />
                    <ClientGridColumn header="" name="" renderer={editRender} />
                    <ClientGridColumn header="" name="" renderer={deleteRender} />
                </ClientGrid>
            </React.Fragment>
        );
    }
}
