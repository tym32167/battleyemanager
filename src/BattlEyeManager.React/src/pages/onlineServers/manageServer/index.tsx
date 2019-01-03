import React from "react";
import { Button, Col, Container, Input, Row } from "reactstrap";
import { ServerHeader } from "../onlineServerHeader";

// interface IManageServersProps {
//     ServerId: number,
//     CommandCallback: (serverId: number, command: string) => void,
// }

interface IManageServersCommandProps {
    command: string,
    commandText: string,
    confirmText: string,
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

            <div className="col-sm-12 col-lg-8 p-2 m-0">
                <div className="bg-white rounded box-shadow p-1">
                    <Container>
                        <MissionSelector />
                    </Container>
                    <Container style={{ 'padding-top': '8px' }}>
                        <Row>
                            <Col xs={12} sm={6} style={{ 'padding-bottom': '8px' }}>
                                <CommandButton commandText="Lock" {...props} />
                                <CommandButton commandText="Unlock" {...props} />
                                <CommandButton commandText="Shutdown" {...props} />
                                <CommandButton commandText="Restart" {...props} />
                                <CommandButton commandText="Restart Server" {...props} />
                            </Col>
                            <Col xs={12} sm={6} style={{ 'padding-bottom': '8px' }}>
                                <CommandButton commandText="Init" {...props} />
                                <CommandButton commandText="Reassign" {...props} />
                                <CommandButton commandText="Load bans" {...props} />
                                <CommandButton commandText="Load scripts" {...props} />
                                <CommandButton commandText="Load events" {...props} />
                            </Col>
                        </Row>
                    </Container>
                </div>
            </div>
        </div>
    </div>
};

const CommandButton = (props: IManageServersCommandProps) => {
    return <React.Fragment>
        <Button color="danger" size="lg" block={true}>{props.commandText}</Button>
    </React.Fragment>
};

const MissionSelector = (props: any) => {
    return <React.Fragment>
        <table>
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
        </table>
    </React.Fragment>
};
