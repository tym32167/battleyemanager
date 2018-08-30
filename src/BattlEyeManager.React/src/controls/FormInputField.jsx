import React from 'react';
import { FormGroup, Label, Input, FormFeedback, FormText } from "reactstrap";
import PropTypes from 'prop-types';

export const FormInputField = (props) => {
    if (props.type === 'checkbox') return (<FormCheckBox {...props} />)
    else return (<FormInput {...props} />)
};

const FormCheckBox = ({
    input,
    label,
    name,
    readOnly,
    type,
    meta: { touched, error, warning }
}) => (
        <FormGroup check>
            <Input {...input} placeholder={label} type={type} readOnly={readOnly}
                invalid={!!error}
                valid={!error} />

            {label && <Label for={name} check>{label}</Label>}

            {touched &&
                ((error && <FormFeedback>{error}</FormFeedback>) ||
                    (warning && <FormText>{warning}</FormText>))}
        </FormGroup>);

const FormInput = ({
    input,
    label,
    name,
    readOnly,
    type,
    meta: { touched, error, warning }
}) => (
        <FormGroup>            
            {label && <Label for={name} check>{label}</Label>}
            <Input {...input} placeholder={label} type={type} readOnly={readOnly}
                invalid={!!error}
                valid={!error} />

            {touched &&
                ((error && <FormFeedback>{error}</FormFeedback>) ||
                    (warning && <FormText>{warning}</FormText>))}
        </FormGroup>);

const propTypes = {
    input: PropTypes.object,
    label: PropTypes.string,
    name: PropTypes.string,
    readOnly: PropTypes.bool,
    type: PropTypes.string,
    meta: PropTypes.shape(
        {
            touched: PropTypes.bool,
            error: PropTypes.string,
            warning: PropTypes.string
        }
    )
}

FormCheckBox.propTypes = propTypes;
FormInput.propTypes = propTypes;