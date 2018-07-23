import React from 'react';
import { Link } from 'react-router-dom';
import { Field, reduxForm } from 'redux-form'
import { Form, Button } from "reactstrap";

import { requiredValidator } from "../../formValidators";
import { FormInputField } from '../../../controls';


const EditUserForm = props => {
    const { handleSubmit, edit } = props;

    return (
        <Form onSubmit={handleSubmit} className="m-1">
            <Field name="lastName" component={FormInputField} type="text" label="Last Name" readOnly={!!edit}
                validate={[requiredValidator]} />
            <Field name="firstName" component={FormInputField} type="text" label="First Name" readOnly={!!edit}
                validate={[requiredValidator]} />
            <Field name="userName" component={FormInputField} type="text" label="User Name" readOnly={!!edit}
                validate={[requiredValidator]} />
            <Field name="email" component={FormInputField} type="email" className="form-control" label="Email"
                validate={[requiredValidator]} />
            <Field name="password" component={FormInputField} type="password" className="form-control" label="Password"
            />

            <Button color="primary" type="submit">Save</Button>
            {' '}            
            <Button tag={Link} to="/users" color="secondary">Cancel</Button>
        </Form>
    );
}

const EditUserFormRedux = reduxForm({
    // a unique name for the form
    form: 'EditUserForm'
})(EditUserForm)

export {EditUserFormRedux as EditUserForm};