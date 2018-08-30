import React from 'react';

import { Form, Button, Modal, ModalBody, ModalHeader, ModalFooter, Input } from 'reactstrap';
import { Field, reset, reduxForm, actionCreators } from 'redux-form'
import { requiredValidator } from '../../formValidators';
import { FormInputField } from '../../../controls';


const KickPlayerForm = ({ handleSubmit, onCancel, kickReasons }) =>
    (<Form onSubmit={handleSubmit} className="mt-1">
        <Field name="kickReason" component={FormInputField} type="textarea" placeholder="Kick reason"
            validate={[requiredValidator]} className="col-sm" />
            {'items: ' + kickReasons.length + ' '}
        <Button color="primary" type="submit">Kick</Button>
        {' '}
        <Button color="secondary" onClick={onCancel}>Cancel</Button>
    </Form>);

const KickPlayerFormRedux = reduxForm({
    // a unique name for the form
    form: 'KickPlayerForm'
})(KickPlayerForm)

export { KickPlayerFormRedux as KickPlayerForm };

