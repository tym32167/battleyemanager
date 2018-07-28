import React from 'react';
import { Field, reset, reduxForm } from 'redux-form'
import { Form, Button } from "reactstrap";
import { requiredValidator } from '../../pages/formValidators';
import PropTypes from 'prop-types';
import { FormInput } from '../FormInput';

const InputMessage = ({ handleSubmit }) =>
    (<Form onSubmit={handleSubmit} inline className="mt-1">
        <Field name="message" component={FormInput} type="text" placeholder="Chat Message"
            validate={[requiredValidator]} className="col-sm" />
        <Button color="primary" type="submit">Send</Button>
    </Form>);


InputMessage.propTypes = {
    handleSubmit: PropTypes.func
}

const afterSubmit = (result, dispatch) =>
  dispatch(reset('ChatMessageForm'));

const InputMessageRedux = reduxForm({
    // a unique name for the form
    form: 'ChatMessageForm',
    onSubmitSuccess: afterSubmit
})(InputMessage)

export { InputMessageRedux as InputMessage };

