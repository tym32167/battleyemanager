import React from "react";
import { connect } from "react-redux";
import { Button, Col, Container, Input, Row } from "reactstrap";
import { Dispatch } from "redux";
import { ConfirmWindow } from "src/controls";
import { ServerHeader } from "../onlineServerHeader";

interface IManageServersProps {
    serverId: number,
    commandCallback: (serverId: number, command: string) => void,
    missionCallback: (serverId: number, mission: string) => void,
    onload: (serverId: number) => void,
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

const ManageServerContent = (props: IManageServersProps) => {

    const callback = (command: string) => {
        props.commandCallback(props.serverId, command);
    }

    const commandProps = {
        callback
    }

    const missionsProps = {
        ...props
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
};


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
    serverId: number
}

const MissionSelector = (props: IMissionSelectorProps) => {
    return <React.Fragment>
        <table>
            <tbody>
                <tr>
                    <td>
                        <Input type="select" name="select" id="exampleSelect">
                            <option>1</option>
                            <option>2</option>
                            <option>3</option>
                            <option>4</option>
                            <option>5</option>
                        </Input>
                    </td>
                    <td><Button color="danger">Set mission</Button>     </td>
                </tr>
            </tbody>
        </table>
    </React.Fragment>
};



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
            // dispatch(onlineMissionsActions.getItems(serverId));
            alert('load: ' + serverId);
        },

        commandCallback: (serverId: number, command: string) => {
            // 
            alert('command: ' + serverId + ' ' + command);
        },
        missionCallback: (serverId: number, mission: string) => {
            // 
            alert('mission: ' + serverId + ' ' + mission);
        }
    }
}

const ConnectedManageServerContent = connect(
    mapStateToProps,
    mapDispatchToProps
)(ManageServerContent);


