import React from 'react';
import { FormGroup, Label, Input, FormFeedback, FormText } from "reactstrap";

export const FormInputField = ({
    input,
    label,
    name,
    readOnly,
    type,
    meta: { touched, error, warning }
}) => (
        <FormGroup>
            <Label for={name}>{label}</Label>
            <Input {...input} placeholder={label} type={type} readOnly={readOnly}
                invalid={!!error}
                valid={!error} />

            {touched &&
                ((error && <FormFeedback>{error}</FormFeedback>) ||
                    (warning && <FormText>{warning}</FormText>))}

        </FormGroup>
    )