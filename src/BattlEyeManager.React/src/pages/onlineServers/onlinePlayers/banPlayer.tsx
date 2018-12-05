import React from 'react';
import { connect } from 'react-redux';
import { Button, Modal, ModalBody, ModalHeader } from 'reactstrap';
import { Dispatch } from 'redux';
import { IOnlinePlayer } from 'src/models';
import { BanPlayerForm } from './banPlayerForm';

interface IBanPlayerProps {
    player: IOnlinePlayer;
    onBan: (player: IOnlinePlayer, { reason, minutes }: { reason: string, minutes: number }) => void;
}

interface IBanPlayerState {
    modal: boolean;
}

class BanPlayer extends React.Component<IBanPlayerProps> {

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
        this.props.onBan(this.props.player, data);
        this.toggle();
    }

    public render() {
        const { player } = this.props;
        const { submit, toggle } = this;

        const p = { onSubmit: submit, onCancel: toggle };

        return (
            <React.Fragment>
                <Button color="danger" size="sm" onClick={this.toggle} >Ban</Button>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}>Ban player {player.name}</ModalHeader>
                    <ModalBody>
                        <BanPlayerForm {...p} />
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ banReasons }: { banReasons: any }) => {
    return {
        error: banReasons.error,
        items: banReasons.items || [],
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
    }
}

const ConnectedBanPlayer = connect(
    mapStateToProps,
    mapDispatchToProps
)(BanPlayer);

export { ConnectedBanPlayer as BanPlayer };