import React, { Component } from 'react';
import { IUser } from 'src/models';
import { history, userService } from 'src/services';
import { Error } from '../../controls';
import { EditUserForm } from './controls';

interface ICreateState {
    error?: any
}

export class Create extends Component<any, ICreateState> {

    constructor(props: any) {
        super(props);
        this.state = {};
    }

    public async Create(item: IUser) {
        await userService.addItem(item).then(
            () => history.push("/users"),
            (error: any) => this.setState({ error }))
    }

    public render() {
        const { error } = this.state;
        const onSubmit = (user: IUser) => {
            this.Create(user);
        }

        return (
            <React.Fragment>
                <h2>Create User</h2>
                <Error error={error} />
                <EditUserForm onSubmit={onSubmit} />
            </React.Fragment>
        );
    }
}