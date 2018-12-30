import React from "react";
import Button from "reactstrap/lib/Button";
import ButtonGroup from "reactstrap/lib/ButtonGroup";
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

            <div className="col-sm-12 col-lg-11  p-2 m-0">
                <div className="bg-white rounded box-shadow p-1">

                    <CommandButton commandText="text" {...props} />
                    <CommandButton commandText="text" {...props} />
                    <CommandButton commandText="text" {...props} />
                    <CommandButton commandText="text" {...props} />

                </div>
            </div>
        </div>
    </div>
};

const CommandButton = (props: IManageServersCommandProps) => {
    return <React.Fragment>
        <ButtonGroup>
            <Button color="danger" size="lg">{props.commandText}</Button>
        </ButtonGroup>
    </React.Fragment>
};
