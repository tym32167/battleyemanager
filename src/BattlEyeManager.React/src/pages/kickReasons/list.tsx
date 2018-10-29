import React, { Component } from 'react';
import { IKickReason } from 'src/models';

interface IKickListProps {
    data: IKickReason[]
}

export class List extends Component<IKickListProps>{

    constructor(props: IKickListProps) {
        super(props);
    }
    public render() {
        return (<React.Fragment>
            <h1>List</h1>
        </React.Fragment>)
    }
}