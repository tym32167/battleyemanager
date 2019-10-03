import React, { Component } from 'react';
import { IUser } from 'src/models';
import { history, userService } from 'src/services';
import { Error } from '../../controls';
import { EditUserForm } from './controls';

interface IEditState {
    item?: IUser,
    error?: any
}

export class Edit extends Component<any, IEditState> {

    constructor(props: any) {
        super(props);
        this.state = {};
    }

    public componentDidMount() {
        this.Load();
    }

    public Load() {
        const { match: { params: { id } } } = (this.props as any);
        userService.getItem(id).then(
            (item: IUser) => this.setState({ item }),
            (error: any) => this.setState({ error }))
    }

    public async Update(item: IUser) {
        userService.updateItem(item).then(
            () => history.push("/users"),
            (error: any) => this.setState({ error }))
    }

    public render() {
        const { item, error } = this.state;
        const onSubmit = (u: IUser) => {
            this.Update(u)
        };

        return (
            <React.Fragment>
                <h2>Edit User</h2>
                <Error error={error} />
                {item && <EditUserForm onSubmit={onSubmit} initialValues={item} {...{ edit: true }} />}
            </React.Fragment>
        );
    }
}

