import React from 'react';
import { connect } from 'react-redux';
import { Button, Modal, ModalBody, ModalHeader } from 'reactstrap';
import { Dispatch } from 'redux';
import { IKickReason, IOnlinePlayer } from 'src/models';
import { kickReasonActions } from '../../../store/actions';
import { KickPlayerForm } from './kickPlayerForm';


interface IKickPlayerProps {
    player: IOnlinePlayer;
    items: IKickReason[];
    onLoad: () => void;

    onKick: (player: IOnlinePlayer, { kickReason }: { kickReason: string }) => void;
}

interface IKickPlayerState {
    modal: boolean;
}



class KickPlayer extends React.Component<IKickPlayerProps> {

    public state: IKickPlayerState;

    constructor(props: IKickPlayerProps) {
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
        if (mod) {
            this.props.onLoad();
        }
    }


    public submit(data: any) {
        this.props.onKick(this.props.player, data);
    }

    public render() {
        const { player } = this.props;
        const { submit, toggle } = this;

        const p = { onSubmit: submit, onCancel: toggle };

        return (
            <React.Fragment>
                <Button color="warning" size="sm" onClick={this.toggle} >Kick</Button>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}>Kick player {player.name}</ModalHeader>
                    <ModalBody>
                        <KickPlayerForm {...p} />
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ kickReasons }: { kickReasons: any }) => {
    return {
        error: kickReasons.error,
        items: kickReasons.items || [],
    }
}

const mapDispatchToProps = (dispatch: Dispatch<void>) => {
    return {
        onLoad: () => dispatch(kickReasonActions.getItems())
    }
}

const ConnectedKickPlayer = connect(
    mapStateToProps,
    mapDispatchToProps
)(KickPlayer);

export { ConnectedKickPlayer as KickPlayer };