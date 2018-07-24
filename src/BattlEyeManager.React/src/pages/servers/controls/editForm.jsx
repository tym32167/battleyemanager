import React from 'react';
import { Link } from 'react-router-dom';
import { Field, reduxForm } from 'redux-form'
import { Form, Button } from "reactstrap";

import { requiredValidator } from "../../formValidators";
import { FormInputField } from '../../../controls';


const EditForm = props => {
    const { handleSubmit, edit } = props;

    return (
        <Form onSubmit={handleSubmit} className="m-1">

            <Field name="name" component={FormInputField} type="text" label="Server Name"
                validate={[requiredValidator]} />
            <Field name="host" component={FormInputField} type="text" label="Host"
                validate={[requiredValidator]} />
            <Field name="port" component={FormInputField} type="number" label="Port"
                validate={[requiredValidator]} />
            <Field name="password" component={FormInputField} type="password" label="Password"/>
            <Field name="steamPort" component={FormInputField} type="number" label="Steam Port"
                validate={[requiredValidator]} />
            <Field name="active" component={FormInputField} type="checkbox" label="Active"
                validate={[requiredValidator]} />

            <Button color="primary" type="submit">Save</Button>
            {' '}
            <Button tag={Link} to="/servers" color="secondary">Cancel</Button>
        </Form>
    );
}

const EditFormRedux = reduxForm({
    // a unique name for the form
    form: 'EditServerForm'
})(EditForm)

export { EditFormRedux as EditForm };