import React, { Component } from "react";
import { connect } from "react-redux";
import { Button, Col, Container, Input, Row } from "reactstrap";
import { Dispatch } from "redux";
import { ConfirmWindow } from "src/controls";
import { IOnlineMission } from "src/models";
import { onlineMissionsService, onlineServerService } from "src/services";
import { onlineMissionActions } from "src/store/actions";
import { ServerHeader } from "../onlineServerHeader";


interface IManageServersProps {
    serverId: number,
    commandCallback: (serverId: number, command: string) => void,
    missionCallback: (mission: IOnlineMission) => void,
    onLoad: (serverId: number) => void,
    items: IOnlineMission[]
}

interface IManageServersCommandProps {
    command: string,
    commandText: string,
    callback: (command: string) => void
}

export const ManageServerControl = (props: any) => {
    return <div className="container-fluid p-lg-3 p-1">
        <div className="row">
            <div className="col-12 p-2 m-0" >
                <div className="bg-white rounded box-shadow  p-1">
                    <ServerHeader {...props} />
                </div>
            </div>

            <ConnectedManageServerContent {...props} />
        </div>
    </div>
};


class ManageServerContent extends Component<IManageServersProps> {
    constructor(props: IManageServersProps) {
        super(props);
        this.refresh = this.refresh.bind(this);
    }

    public componentDidMount() {
        this.refresh();
    }

    public refresh() {
        const { serverId } = this.props;
        this.props.onLoad(serverId);
    }

    public render() {
        const p = this.props;
        const callback = (command: string) => {
            p.commandCallback(p.serverId, command);
        }

        const commandProps = {
            callback
        }

        const missionsProps = {
            ...p
        }

        return <div className="col-sm-12 col-lg-8 p-2 m-0">
            <div className="bg-white rounded box-shadow p-1">
                <Container>
                    <MissionSelector {...missionsProps} />
                </Container>
                <Container style={{ 'paddingTop': '8px' }}>
                    <Row>
                        <Col xs={12} sm={6} style={{ 'paddingBottom': '8px' }}>
                            <CommandButton commandText="Lock" command="lock" {...commandProps} />
                            <CommandButton commandText="Unlock" command="unlock" {...commandProps} />
                            <CommandButton commandText="Shutdown" command="shutdown" {...commandProps} />
                            <CommandButton commandText="Restart" command="restart" {...commandProps} />
                            <CommandButton commandText="Restart Server" command="restartserver" {...commandProps} />
                        </Col>
                        <Col xs={12} sm={6} style={{ 'paddingBottom': '8px' }}>
                            <CommandButton commandText="Init" command="init" {...commandProps} />
                            <CommandButton commandText="Reassign" command="reassign" {...commandProps} />
                            <CommandButton commandText="Load bans" command="loadbans" {...commandProps} />
                            <CommandButton commandText="Load scripts" command="loadscripts" {...commandProps} />
                            <CommandButton commandText="Load events" command="loadevents" {...commandProps} />
                        </Col>
                    </Row>
                </Container>
            </div>
        </div>
    }
}


const CommandButton = (props: IManageServersCommandProps) => {

    const renderer = (toggle: () => void) => <Button color="danger" size="lg" onClick={toggle} block={true} >{props.commandText}</Button>;
    const submit = () => {
        props.callback(props.command)
    };

    return <React.Fragment>
        <ConfirmWindow
            submit={submit}
            text={'Execute command ' + props.commandText + '?'}
            title="Execute command"
            buttonRenderer={renderer}
        />

    </React.Fragment>
};

interface IMissionSelectorProps {
    serverId: number,
    missionCallback: (mission: IOnlineMission) => void,
    items: IOnlineMission[]
}

interface IMissionSelectorState {
    mission?: IOnlineMission
}


// tslint:disable-next-line:max-classes-per-file
class MissionSelector extends Component<IMissionSelectorProps, IMissionSelectorState> {

    constructor(props: IMissionSelectorProps) {
        super(props);
        this.state = {};
        if (props && props.items && props.items.length > 0) {
            this.state = { mission: props.items[0] };
        }

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    public handleChange(event: React.ChangeEvent<HTMLInputElement>) {
        const mission = this.props.items.find(x => x.name === event.target.value);
        this.setState({ mission });
    }

    public handleSubmit() {
        const { mission } = this.state;
        if (mission) {
            this.props.missionCallback(mission);
        }
    }

    public componentWillReceiveProps(nextProps: IMissionSelectorProps) {
        const p = nextProps;
        if (p && p.items && p.items.length > 0) {
            this.setState({ mission: p.items[0] });
        }
    }

    public render() {
        const { items } = this.props;
        const renderer = (toggle: () => void) => <Button color="danger" onClick={toggle} >Set mission</Button>;
        return (<React.Fragment>
            <table>
                <tbody>
                    <tr>
                        <td>
                            <Input type="select" name="select" id="missionsSelect" onChange={this.handleChange}>
                                {items && items.map((m, i) => (<option key={i}>{m.name}</option>))}
                            </Input>
                        </td>
                        <td>
                            <ConfirmWindow
                                submit={this.handleSubmit}
                                text={'Do you want to setup mission ' + (this.state.mission ? this.state.mission.name : '') + '?'}
                                title="Set mission"
                                buttonRenderer={renderer}
                            />
                        </td>
                    </tr>
                </tbody>
            </table>
        </React.Fragment>);
    }
}

const mapStateToProps = ({ onlineMissions }: { onlineMissions: any }, ownProps: any) => {
    const server = onlineMissions && onlineMissions[ownProps.match.params.serverId];
    let items = [];
    if (server &&
        server.items) { items = server.items; }

    let error;
    if (server &&
        server.error) { error = server.error; }

    return {
        busy: server && server.busy,
        error,
        items,
        serverId: ownProps.match.params.serverId,
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        onLoad: (serverId: number) => {
            dispatch(onlineMissionActions.getItems(serverId));
        },

        commandCallback: (serverId: number, command: string) => {
            // onlineServerService.getItems();
            onlineServerService.sendCommand(serverId, command);
        },
        missionCallback: (mission: IOnlineMission) => {
            // 
            onlineMissionsService.setMission(mission);
        }
    }
}

const ConnectedManageServerContent = connect(
    mapStateToProps,
    mapDispatchToProps
)(ManageServerContent);


