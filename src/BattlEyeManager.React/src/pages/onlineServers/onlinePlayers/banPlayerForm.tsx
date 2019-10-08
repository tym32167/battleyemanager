import React from 'react';
import { Trans } from 'react-i18next';
import { Button, Form } from 'reactstrap';
import { Field, InjectedFormProps, reduxForm } from 'redux-form'
import { FormInputField } from '../../../controls';
import { requiredValidator } from '../../formValidators';

interface IBanPlayerFormProps extends InjectedFormProps {
    onCancel: () => void;
}


const BanPlayerForm = ({ handleSubmit, onCancel }: IBanPlayerFormProps) =>
    (<Form onSubmit={handleSubmit} className="mt-1">
        <Field name="reason" component={FormInputField} type="textarea" placeholder="Ban reason"
            validate={[requiredValidator]} className="col-sm" />

        <Field name="minutes" component={FormInputField} type="text" placeholder="Minutes"
            validate={[requiredValidator]} className="col-sm" />

        <Button color="primary" type="submit"><Trans>Ban</Trans></Button>
        {' '}
        <Button color="secondary" onClick={onCancel}><Trans>Cancel</Trans></Button>
    </Form>);

const BanPlayerFormRedux = reduxForm({
    // a unique name for the form
    form: 'BanPlayerForm'
})(BanPlayerForm)

export { BanPlayerFormRedux as BanPlayerForm };

