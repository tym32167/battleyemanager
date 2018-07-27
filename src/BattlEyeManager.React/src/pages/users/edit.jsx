import React, { Component } from 'react';
import { connect } from "react-redux";
import { userActions } from "../../store/actions";
import { EditUserForm } from './controls';
import { Error } from '../../controls';
import PropTypes from 'prop-types';

class Edit extends Component {

    componentDidMount() {
        this.props.onLoad(this.props.id);
    }

    render() {
        const { user, onSubmit, error } = this.props;

        return (
            <React.Fragment>
                <h2>Edit User</h2>
                <Error error={error} />
                {user && <EditUserForm onSubmit={onSubmit} initialValues={user} edit={true} />}
            </React.Fragment>
        );
    }
}

Edit.propTypes = {
    onLoad : PropTypes.func.isRequired,
    id:PropTypes.string.isRequired,
    user: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    error: PropTypes.object
}

const mapStateToProps = ({ users }, ownProps) => {
    return {
        user: users.item,
        id: ownProps.match.params.id,
        error: users.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (id) => dispatch(userActions.getItem(id)),
        onSubmit: (user) => dispatch(userActions.updateItem(user))
    }
}

const ConnectedEdit = connect(
    mapStateToProps,
    mapDispatchToProps
)(Edit);

export { ConnectedEdit as Edit };