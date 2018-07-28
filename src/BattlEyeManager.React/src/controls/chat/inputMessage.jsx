import React from 'react';
import { Field, reduxForm } from 'redux-form'
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

const InputMessageRedux = reduxForm({
    // a unique name for the form
    form: 'ChatMessageForm'
})(InputMessage)

export { InputMessageRedux as InputMessage };

