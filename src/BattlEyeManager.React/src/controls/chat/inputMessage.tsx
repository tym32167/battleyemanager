import React, { Dispatch } from 'react';
import { Trans } from 'react-i18next';
import { Button, Form } from "reactstrap";
import { Field, reduxForm, reset } from 'redux-form'
import { requiredValidator } from '../../pages/formValidators';
import { FormInput } from '../FormInput';

export interface IInputMessageProps {
    handleSubmit: (e: React.FormEvent<HTMLFormElement>) => void
}

const InputMessage = (props: IInputMessageProps) => {
    const { handleSubmit } = props;
    return (<Form onSubmit={handleSubmit} inline={true} className="mt-1">
        <Field name="message" component={FormInput} type="text" placeholder="Chat Message"
            validate={[requiredValidator]} className="col-sm" />
        <Button color="primary" type="submit"><Trans>Send</Trans></Button>
    </Form>)
};

const afterSubmit = (result: any, dispatch: Dispatch<any>) =>
    dispatch(reset('ChatMessageForm'));

const InputMessageRedux = reduxForm({
    // a unique name for the form
    form: 'ChatMessageForm',
    onSubmitSuccess: afterSubmit
})(InputMessage)

export { InputMessageRedux as InputMessage };

