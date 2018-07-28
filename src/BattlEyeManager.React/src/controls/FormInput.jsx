import React from 'react';
import { Input } from "reactstrap";
import PropTypes from 'prop-types';

export const FormInput = ({
    input,
    label,
    readOnly,
    type,
    className,
    meta: { touched, error }
}) => (
        <Input {...input} className={className} placeholder={label} type={type} readOnly={readOnly}
            invalid={!!touched && !!error}
            valid={!touched && !error} />);

const propTypes = {
    input: PropTypes.object,
    label: PropTypes.string,
    readOnly: PropTypes.bool,
    type: PropTypes.string,
    className: PropTypes.string,
    meta: PropTypes.shape(
        {
            touched: PropTypes.bool,
            error: PropTypes.string
        }
    )
}

FormInput.propTypes = propTypes;