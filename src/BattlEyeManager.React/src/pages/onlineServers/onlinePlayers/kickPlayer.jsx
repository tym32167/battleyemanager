import React from 'react';
import { Button, Modal, ModalBody, ModalHeader, ModalFooter } from 'reactstrap';
import { kickReasonActions } from '../../../store/actions';
import {connect} from 'react-redux';


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

        if(mod){
            this.props.onLoad();
        }

    }

    render() {
        const {player} = this.props;
        return (
            <React.Fragment>
                <Button color="warning" size="sm" onClick={this.toggle} >Kick</Button>
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}>Kick player {player.name}</ModalHeader>
                    <ModalBody>
                        Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                    </ModalBody>
                    <ModalFooter>
                        <Button color="primary" onClick={this.toggle}>Do Something</Button>{' '}
                        <Button color="secondary" onClick={this.toggle}>Cancel</Button>
                    </ModalFooter>
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