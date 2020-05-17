import React from 'react';
import { Link } from 'react-router-dom';
import { Field, reduxForm } from 'redux-form'
import { Form, Button } from "reactstrap";
import { Trans } from 'react-i18next';

import { requiredValidator } from "../../formValidators";
import { FormInputField } from '../../../controls';

import PropTypes from 'prop-types';

const EditForm = props => {
    const { handleSubmit } = props;

    return (
        <Form onSubmit={handleSubmit} className="m-1">

            <Field name="name" component={FormInputField} type="text" label="Server Name"
                validate={[requiredValidator]} />
            <Field name="host" component={FormInputField} type="text" label="Host"
                validate={[requiredValidator]} />
            <Field name="port" component={FormInputField} type="number" label="Port"
                validate={[requiredValidator]} />
            <Field name="password" component={FormInputField} type="password" label="Password" />
            <Field name="steamPort" component={FormInputField} type="number" label="Steam Port"
                validate={[requiredValidator]} />

            <Field name="welcomeFeatureEnabled" component={FormInputField} type="checkbox" label="Welcome Feature Enabled" />
            <Field name="welcomeFeatureTemplate" component={FormInputField} type="text" label="Welcome Feature Template (Welcome, {name}! {sessions} sessions and {hours} hours on server!)" />
            <Field name="welcomeFeatureEmptyTemplate" component={FormInputField} type="text" label="Welcome Feature Empty Template (Welcome, {name}! First time on server!)" />

            <Field name="active" component={FormInputField} type="checkbox" label="Active" />

            <Button color="primary" type="submit"><Trans>Save</Trans></Button>
            {' '}
            <Button tag={Link} to="/servers" color="secondary"><Trans>Cancel</Trans></Button>
        </Form>
    );
}

EditForm.propTypes = {
    handleSubmit: PropTypes.func.isRequired
}

const EditFormRedux = reduxForm({
    // a unique name for the form
    form: 'EditServerForm'
})(EditForm)

export { EditFormRedux as EditForm };