import React from 'react';
import { Trans } from 'react-i18next';
import { Button, Form } from "reactstrap";
import { Field, reduxForm } from 'redux-form'
import { FormInputField } from '../../../controls';
import { requiredValidator } from "../../formValidators";

const EditServerOptionsForm = (props: any) => {
    const { handleSubmit } = props;
    return (
        <Form onSubmit={handleSubmit} className="m-1">
            <Field name="thresholdFeatureEnabled" component={FormInputField} type="checkbox" label="Threshold Feature Enabled" />
            <Field name="thresholdMinHoursCap" component={FormInputField} type="number" label="Threshold min hours cap" />
            <Field name="thresholdFeatureMessageTemplate" component={FormInputField} validate={[requiredValidator]} type="text" label="Threshold Feature Message Template" />

            <Button color="primary" type="submit"><Trans>Save</Trans></Button>
        </Form>
    );
}

const EditServerOptionsFormRedux = reduxForm({
    // a unique name for the form
    form: 'EditServerOptionsForm'
})(EditServerOptionsForm)

export { EditServerOptionsFormRedux as EditServerOptionsForm };