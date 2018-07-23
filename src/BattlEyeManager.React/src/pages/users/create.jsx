import React, { Component } from 'react';
import { connect } from "react-redux";
import { userActions } from "../../store/actions";
import { EditUserForm } from './controls';

class Create extends Component {    
    render() {
        const { onSubmit } = this.props;

        return (
            <React.Fragment>
                <h2>Create User</h2>
                <EditUserForm onSubmit={onSubmit} />
            </React.Fragment>
        );
    }
}

const mapStateToProps = () => {
    return {}
}

const mapDispatchToProps = (dispatch) => {
    return {        
        onSubmit: (user) => dispatch(userActions.addUser(user))
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(Create);

export { ConnectedCreate as Create };