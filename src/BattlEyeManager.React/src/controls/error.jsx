import React from 'react';
import { Alert } from 'reactstrap';

export const Error = ({ error }) => {

    if (!error || error === '' || error.length === 0) return '';
    if (error.error) return <Error error={error.error} />;

    if (error.errors.map) return error.errors.map((e, i) => <Error key={i} error={error.errors[i]} />);

    if (typeof (error) === 'string')
        return (<Alert color="danger">{error}</Alert>);

    return '';
}