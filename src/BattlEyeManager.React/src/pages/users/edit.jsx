import React, { Component } from 'react';
import { connect } from "react-redux";
import { userActions } from "../../store/actions";
import { EditUserForm } from './controls';

class Edit extends Component {

    componentDidMount() {
        this.props.onLoad(this.props.id);
    }

    render() {
        const { user, onSubmit } = this.props;

        return (
            <div className="my-3 p-3 bg-white rounded box-shadow col-12 col-md-7 ">
                <h2>Edit User</h2>
                {user && <EditUserForm onSubmit={onSubmit} initialValues={user} edit={true} />}
            </div>
        );
    }
}

const mapStateToProps = ({ user }, ownProps) => {
    return {
        user: user.user,
        id: ownProps.match.params.id
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