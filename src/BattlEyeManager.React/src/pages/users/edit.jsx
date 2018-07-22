import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Field, reduxForm } from 'redux-form'
import {connect} from "react-redux";
import {userActions} from "../../actions";


class Edit extends Component {

    componentDidMount() {
        this.props.onLoad(this.props.id);
    }

    submit = values => {
        // print the form values to the console
        console.log(values)
      }

    render() {
        return (
            <div className="my-3 p-3 bg-white rounded box-shadow">
                <h2>Edit User</h2>
                <Link to="/users">Back to list</Link>
                <EditUserForm onSubmit={this.submit} />
            </div>
        );
    }
}

let EditUserForm = props => {
    const { handleSubmit } = props
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="firstName">First Name</label>
                <Field name="firstName" component="input" type="text" />
            </div>
            <div>
                <label htmlFor="lastName">Last Name</label>
                <Field name="lastName" component="input" type="text" />
            </div>
            <div>
                <label htmlFor="email">Email</label>
                <Field name="email" component="input" type="email" />
            </div>
            <button type="submit">Submit</button>
        </form>
    );
}

EditUserForm = reduxForm({
    // a unique name for the form
    form: 'EditUserForm'
})(EditUserForm)


const mapStateToProps = ({user}, ownProps) => {
    return {
        user: user.user,
        id:ownProps.match.params.id
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (id) => dispatch(userActions.getUser(id))
    }
}

const ConnectedEdit = connect(
    mapStateToProps,
    mapDispatchToProps
)(Edit);

export {ConnectedEdit as Edit};