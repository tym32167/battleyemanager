import React, { Component } from "react";
import { IServerOptions } from "src/models";
import { history, onlineServerService } from "src/services";
import { ServerHeader } from "../onlineServerHeader";
import { EditServerOptionsForm } from "./editForm";
import { Error } from '../../../controls';


interface IServerOptionsState {
    serverOptions?: IServerOptions,
    error?: any
}


export const ServerOptionsControl = (props: any) => {
    return <div className="container-fluid p-lg-3 p-1">
        <div className="row">
            <div className="col-12 p-2 m-0" >
                <div className="bg-white rounded box-shadow  p-1">
                    <ServerHeader {...props} />
                </div>
            </div>

            <div className="col-sm-12 col-lg-11  p-2 m-0">
                <div className="bg-white rounded box-shadow p-1">
                    <ServerOptionsComponent {...props} />
                </div>
            </div>
        </div>
    </div>
};

class ServerOptionsComponent extends Component<any, IServerOptionsState>{
    constructor(props: any) {
        super(props);
        this.state = {};
    }

    public componentDidMount() {
        this.Load();
    }

    public Load() {
        const { match: { params: { serverId } } } = (this.props as any);
        onlineServerService.getServerOptions(serverId).then(
            (item) => this.setState({ serverOptions: item }),
            (error) => this.setState({ error }));
    }

    public async Update(item: IServerOptions) {
        onlineServerService.saveServerOptions(item).then(
            () => history.push("/online/" + item.id),
            (error: any) => this.setState({ error }));
    }

    public render() {

        const { serverOptions, error } = this.state;
        const onSubmit = (u: IServerOptions) => {
            this.Update(u)
        };

        return (
            <React.Fragment>
                <Error error={error} />
                {serverOptions && <EditServerOptionsForm onSubmit={onSubmit} initialValues={serverOptions} {...{ edit: true }} />}
            </React.Fragment>
        )
    }
}