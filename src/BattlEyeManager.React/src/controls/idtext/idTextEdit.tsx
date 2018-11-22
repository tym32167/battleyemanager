import React, { Component } from 'react';
import { IIdTextItem } from 'src/models';

import { Link } from 'react-router-dom';
import { Button, Form } from "reactstrap";
import { Field, InjectedFormProps, reduxForm } from 'redux-form'
import { FormInputField } from '../../controls';
import { requiredValidator } from "../../pages/formValidators";

interface IEditProps<T> extends InjectedFormProps {
    item: T,
    error: any,
    title: string,
    form: string,
    listUrl: string,
    onSubmit: (item: IIdTextItem) => void
}

export class IdTextEdit extends Component<IEditProps<IIdTextItem>>{

    public render() {
        const { title, form, item, onSubmit, listUrl } = this.props;
        const formProps = { onSubmit, initialValues: item, listUrl };

        const IdTextEditFormRedux = reduxForm({
            // a unique name for the form
            form
        })(IdTextEditForm)

        return (
            <React.Fragment>
                <h2>{title} </h2>
                <IdTextEditFormRedux {...formProps} />
            </React.Fragment>
        );
    }
}

const IdTextEditForm = (props: IEditProps<IIdTextItem>) => {
    const { handleSubmit, listUrl } = props;
    return (
        <Form onSubmit={handleSubmit} className="m-1">
            <Field name="id" component={FormInputField} type="text" label="Id" readOnly={true} />
            <Field name="text" component={FormInputField} type="text" className="form-control" label="Text"
                validate={[requiredValidator]} />
            <Button color="primary" type="submit">Save</Button>
            {' '}
            <Button tag={Link} to={listUrl} color="secondary">Cancel</Button>
        </Form>

    );
}
