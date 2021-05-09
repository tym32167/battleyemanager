import React from 'react';
import { Trans } from 'react-i18next';
import { Button, Modal, ModalBody, ModalHeader } from 'reactstrap';
import { BanPlayerForm } from '../onlinePlayers/banPlayerForm';


interface IBanPlayerProps {
    playerId: number,
    playerName: string
    onBan: (playerId: number, { reason, minutes }: { reason: string, minutes: number }) => void;
}

interface IBanPlayerState {
    modal: boolean;
}

export class BanPlayerOffline extends React.Component<IBanPlayerProps> {

    public state: IBanPlayerState;

    constructor(props: IBanPlayerProps) {
        super(props);
        this.state = {
            modal: false,
        };

        this.toggle = this.toggle.bind(this);
        this.submit = this.submit.bind(this);
    }

    public toggle() {
        const mod = !this.state.modal;
        this.setState({
            modal: mod
        });
    }

    public submit(data: any) {
        this.props.onBan(this.props.playerId, data);
        this.toggle();
    }

    public render() {
        const { playerName } = this.props;
        const { submit, toggle } = this;
        const p = { onSubmit: submit, onCancel: toggle };

        return (
            <React.Fragment>
                <Button color="danger" size="sm" onClick={this.toggle} ><Trans>Ban</Trans></Button>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}><Trans>Ban player</Trans> {playerName}</ModalHeader>
                    <ModalBody>
                        <BanPlayerForm {...p} />
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}
