import React, { Component } from 'react';
import { connect } from "react-redux";
import { userActions } from "../../store/actions";
import { EditUserForm } from './controls';
import {Error} from '../../controls';

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

const mapStateToProps = ({ users }, ownProps) => {
    return {
        user: users.user,
        id: ownProps.match.params.id,
        error: users.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (id) => dispatch(userActions.getUser(id)),
        onSubmit: (user) => dispatch(userActions.updateUser(user))
    }
}

const ConnectedEdit = connect(
    mapStateToProps,
    mapDispatchToProps
)(Edit);

export { ConnectedEdit as Edit };