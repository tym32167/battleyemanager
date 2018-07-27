import React, { Component } from 'react';
import { connect } from "react-redux";
import { userActions } from "../../store/actions";
import { EditUserForm } from './controls';
import { Error } from '../../controls';
import PropTypes from 'prop-types';

class Create extends Component {
    render() {
        const { onSubmit, error } = this.props;

        return (
            <React.Fragment>
                <h2>Create User</h2>
                <Error error={error} />
                <EditUserForm onSubmit={onSubmit} />
            </React.Fragment>
        );
    }
}

Create.propTypes = {
    onSubmit: PropTypes.func.isRequired,
    error: PropTypes.object
}

const mapStateToProps = ({ users }) => {
    return {
        error: users.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onSubmit: (user) => dispatch(userActions.addItem(user))
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(Create);

export { ConnectedCreate as Create };