import React from 'react';
import { Button, Modal, ModalBody, ModalHeader, ModalFooter } from 'reactstrap';
import { kickReasonActions } from '../../../store/actions';
import { connect } from 'react-redux';
import { KickPlayerForm } from './kickPlayerForm';


class KickPlayer extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            modal: false
        };

        this.toggle = this.toggle.bind(this);
    }

    toggle() {

        const mod = !this.state.modal;

        this.setState({
            modal: mod
        });

        if (mod) {
            this.props.onLoad();
        }

    }

    render() {
        const { player, handleSubmit, items } = this.props;
        return (
            <React.Fragment>
                <Button color="warning" size="sm" onClick={this.toggle} >Kick</Button>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}>Kick player {player.name}</ModalHeader>
                    <ModalBody>    
                        <KickPlayerForm onSubmit={()=>{alert('kjhj'); return false;}} onCancel={this.toggle} 
                            kickReasons={items} /> 
                    </ModalBody>                    
                </Modal>
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({ kickReasons }) => {
    return {
        items: kickReasons.items || [],
        error: kickReasons.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: () => dispatch(kickReasonActions.getItems())
    }
}

const ConnectedKickPlayer = connect(
    mapStateToProps,
    mapDispatchToProps
)(KickPlayer);

export { ConnectedKickPlayer as KickPlayer };