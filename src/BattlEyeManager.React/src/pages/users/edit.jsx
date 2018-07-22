import React, {Component} from 'react';
import {Link} from 'react-router-dom';
import {Field, reduxForm} from 'redux-form'
import {connect} from "react-redux";
import {userActions} from "../../actions";

import {requiredValidator} from "../formValidators";


class Edit extends Component {

    componentDidMount() {
        this.props.onLoad(this.props.id);
    }

    render() {
        const {user, onSubmit} = this.props;

        return (
            <div className="my-3 p-3 bg-white rounded box-shadow .col-md-12">
                <h2>Edit User</h2>
                <Link to="/users">Back to list</Link>

                {user && <EditUserForm onSubmit={onSubmit} initialValues={user}/>}
            </div>
        );
    }
}

const RenderField = ({
                         input,
                         label,
                         name,
                         readOnly,
                         type,
                         meta: {touched, error, warning}
                     }) => (
    <div className="form-group row">
        <label htmlFor={name} className="col-sm-2 col-form-label">{label}</label>
        <div className="col-sm-10">
            <input {...input} placeholder={label} type={type} className="form-control" readOnly={readOnly}/>
            {touched &&
            ((error && <span>{error}</span>) ||
                (warning && <span>{warning}</span>))}
        </div>
    </div>
)


let EditUserForm = props => {
    const {handleSubmit} = props;

    return (
        <form onSubmit={handleSubmit}>
            <Field name="lastName" component={RenderField} type="text" label="Last Name" readOnly={true}
                   validate={[requiredValidator]}/>
            <Field name="firstName" component={RenderField} type="text" label="First Name" readOnly={true}
                   validate={[requiredValidator]}/>
            <Field name="userName" component={RenderField} type="text" label="User Name" readOnly={true}
                   validate={[requiredValidator]}/>
            <Field name="email" component={RenderField} type="email" className="form-control" label="Email"
                   validate={[requiredValidator]}/>
            <Field name="password" component={RenderField} type="password" className="form-control" label="Password"
                   />
            <button type="submit">Save</button>
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
        id: ownProps.match.params.id
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onLoad: (id) => dispatch(userActions.getUser(id)),
        onSubmit: (user)=> dispatch(userActions.updateUser(user))
    }
}

const ConnectedEdit = connect(
    mapStateToProps,
    mapDispatchToProps
)(Edit);

export {ConnectedEdit as Edit};