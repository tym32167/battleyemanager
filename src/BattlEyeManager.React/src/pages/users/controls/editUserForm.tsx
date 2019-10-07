import React from 'react';
import { Link } from 'react-router-dom';
import { Button, Form } from "reactstrap";
import { Field, reduxForm } from 'redux-form'
import { FormInputField } from '../../../controls';
import { requiredValidator } from "../../formValidators";

const EditUserForm = (props: any) => {
    const { handleSubmit, edit } = props;

    return (
        <Form onSubmit={handleSubmit} className="m-1">
            <Field name="lastName" component={FormInputField} type="text" label="Last Name" readOnly={!!edit}
                validate={[requiredValidator]} />
            <Field name="firstName" component={FormInputField} type="text" label="First Name" readOnly={!!edit}
                validate={[requiredValidator]} />
            <Field name="userName" component={FormInputField} type="text" label="User Name" readOnly={!!edit}
                validate={[requiredValidator]} />
            <Field name="displayName" component={FormInputField} type="text" label="Display Name"
                validate={[requiredValidator]} />
            <Field name="email" component={FormInputField} type="email" className="form-control" label="Email"
                validate={[requiredValidator]} />
            <Field name="password" component={FormInputField} type="password" className="form-control" label="Password" />
            <Field name="isAdmin" component={FormInputField} type="checkbox" className="form-control" label="Is Admin" />
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

export { EditUserFormRedux as EditUserForm };