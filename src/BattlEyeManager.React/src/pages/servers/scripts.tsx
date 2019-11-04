import i18next from 'i18next';
import React, { Component } from 'react';
import { Trans, withTranslation } from 'react-i18next';
import { Button, Form, FormGroup, Input, Label, Modal, ModalBody, ModalHeader } from 'reactstrap';
import { ClientGrid, ClientGridColumn, ClientGridColumns } from 'src/controls';
import { IServer, IServerScriptItem } from 'src/models';
import { serverScriptService, serverSimpleService } from 'src/services';
import { Error } from '../../controls';

interface IServerScriptListProps {
    t: i18next.TFunction
}

interface IServerScriptListState {
    data?: IServerScriptItem[],
    server?: IServer,
    error: any
}

class ServerScriptList extends Component<IServerScriptListProps, IServerScriptListState>{
    constructor(props: any) {
        super(props);
        this.state = { data: undefined, error: undefined };
        this.Load = this.Load.bind(this);
        this.Update = this.Update.bind(this);
        this.Add = this.Add.bind(this);
        this.Delete = this.Delete.bind(this);
    }

    public componentDidMount() {
        const { match: { params: { serverId } } } = (this.props as any);

        serverSimpleService.getItem(serverId).then(server => {
            this.setState({ ...this.state, server });
        });

        this.Load();
    }

    public async Load() {
        const { match: { params: { serverId } } } = (this.props as any);

        await serverScriptService.getItems(serverId).then(
            (items: IServerScriptItem[]) => {
                this.setState({ data: items, error: undefined });
            },
            (error: any) => this.setState({ data: undefined, error }));
    }

    public async Update(item: IServerScriptItem) {
        await serverScriptService.updateItem(item).then(
            () => {
                this.Load();
            },
            (error: any) => this.setState({ data: undefined, error }));
    }

    public async Add(item: IServerScriptItem) {
        await serverScriptService.addItem(item).then(
            () => {
                this.Load();
            },
            (error: any) => this.setState({ ...this.state, data: undefined, error }));
    }

    public async Delete(item: IServerScriptItem) {

        if (!window.confirm('Are you sure you want to delete ' + item.name + '?')) {
            return;
        }

        await serverScriptService.deleteItem(item).then(
            () => {
                this.Load();
            },
            (error: any) => this.setState({ ...this.state, data: undefined, error }));
    }

    public async Run(item: IServerScriptItem) {

        if (!window.confirm('Are you sure you want to run ' + item.name + '?')) {
            return;
        }

        await serverScriptService.runItem(item).then(
            () => {
                window.alert(item.name + ' executed.');
            },
            (error: any) => this.setState({ ...this.state, data: undefined, error }));
    }

    public render() {
        const { data, error, server } = this.state;
        const { t } = this.props;
        let header = t("Server scripts");

        if (server) {
            header = t("Server scripts for ") + server.name;
        }

        const { match: { params: { serverId } } } = (this.props as any);

        const editRender = (item: IServerScriptItem) => <EditServerScript key={item.id} isEdit={true} callback={this.Update} item={item} />;

        // tslint:disable-next-line: jsx-no-lambda
        const runRender = (item: IServerScriptItem) => <Button key={item.id} color="danger" size="sm" onClick={e => this.Run(item)}><Trans>RUN</Trans></Button>;

        // tslint:disable-next-line: jsx-no-lambda
        const deleteRender = (item: IServerScriptItem) => <Button key={item.id} color="danger" size="sm" onClick={e => this.Delete(item)}><Trans>Delete</Trans></Button>;
        const beforeGrid = () => <EditServerScript isCreate={true} callback={this.Add} item={{ id: 0, serverId, name: "", path: "" }} />;

        return (
            <React.Fragment>
                <Error error={error} />
                <ClientGrid data={data} error={error} header={header} showLen={false} enableSort={true} beforeGrid={beforeGrid} >
                    <ClientGridColumns>
                        <ClientGridColumn header="Name" name="name" headerStyle={{ width: '1%' }} />
                        <ClientGridColumn header="Path" name="path" />
                        <ClientGridColumn header="Edit" name="id" renderer={editRender} headerStyle={{ width: '1%' }} />
                        <ClientGridColumn header="Delete" name="id" renderer={deleteRender} headerStyle={{ width: '1%' }} />
                        <ClientGridColumn header="Run" name="id" renderer={runRender} headerStyle={{ width: '1%' }} />
                    </ClientGridColumns>
                </ClientGrid>
            </React.Fragment>
        );
    }
}



interface IEditServerScriptProps {
    item: IServerScriptItem,
    callback: (item: IServerScriptItem) => void,
    isEdit?: boolean,
    isCreate?: boolean
}

interface IEditServerScriptState {
    modal: boolean,
    item: IServerScriptItem
}

// tslint:disable-next-line: max-classes-per-file
class EditServerScript extends Component<IEditServerScriptProps, IEditServerScriptState>
{
    constructor(props: IEditServerScriptProps) {
        super(props);
        this.state = { modal: false, item: props.item };

        this.handleChange = this.handleChange.bind(this);
        this.toggle = this.toggle.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    public handleChange(e: React.FormEvent<EventTarget>) {
        const target = e.target as HTMLInputElement;
        const { name, value } = target;
        const newItem = { ...this.state.item, [name]: value }
        this.setState({ ...this.state, item: newItem });
    }

    public toggle() {
        const mod = !this.state.modal;
        this.setState({
            modal: mod
        });
    }

    public handleSubmit(e: any) {
        e.preventDefault();
        const { callback } = this.props;
        const { item } = this.state;
        if (item.name && item.path) {
            if (callback) {
                callback(item);
            }
            this.toggle();
        }
    }

    public render() {
        const { modal, item } = this.state;
        const canSubmit = item.name !== undefined && item.name !== "" && item.path !== undefined && item.path !== "";

        return (
            <React.Fragment>
                {this.props.isEdit && <Button color="success" size="sm" onClick={this.toggle} ><Trans>Edit</Trans></Button>}
                {this.props.isCreate && <Button color="primary" size="sm" onClick={this.toggle} ><Trans>Create</Trans></Button>}

                <Modal isOpen={modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}><Trans>Edit script</Trans> {item.name}</ModalHeader>
                    <ModalBody>
                        <Form onSubmit={this.handleSubmit} >
                            <FormGroup>
                                <Label><Trans>Name</Trans></Label>
                                <Input value={item.name} name="name" onChange={this.handleChange} />
                            </FormGroup>
                            <FormGroup>
                                <Label><Trans>Path</Trans></Label>
                                <Input value={item.path} name="path" onChange={this.handleChange} />
                            </FormGroup>
                            <Button color="success" disabled={!canSubmit}><Trans>Save</Trans></Button>
                        </Form>

                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}


const ServerScriptsTranslated = withTranslation()(ServerScriptList)
export { ServerScriptsTranslated as ServerScriptList };