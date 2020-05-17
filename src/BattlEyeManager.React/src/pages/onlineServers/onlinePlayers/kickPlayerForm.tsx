import React from 'react';
import { Trans } from 'react-i18next';
import { Button, Form } from 'reactstrap';
import { Field, InjectedFormProps, reduxForm } from 'redux-form'
import { FormInputField } from '../../../controls';
import { requiredValidator } from '../../formValidators';

interface IKickPlayerFormProps extends InjectedFormProps {
    onCancel: () => void;
}
const KickPlayerForm = ({ handleSubmit, onCancel }: IKickPlayerFormProps) =>
    (<Form onSubmit={handleSubmit} className="mt-1">
        <Field name="kickReason" component={FormInputField} type="textarea" placeholder="Kick reason"
            validate={[requiredValidator]} className="col-sm" />
        <Button color="primary" type="submit"><Trans>Kick</Trans></Button>
        {' '}
        <Button color="secondary" onClick={onCancel}><Trans>Cancel</Trans></Button>
    </Form>);

const KickPlayerFormRedux = reduxForm({
    // a unique name for the form
    form: 'KickPlayerForm'
})(KickPlayerForm)

export { KickPlayerFormRedux as KickPlayerForm };

