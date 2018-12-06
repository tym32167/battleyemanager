import React from 'react';
import { Button, Form, Modal, ModalBody, ModalHeader } from 'reactstrap';

interface IConfirmWindowProps {
    text: string,
    title: string,
    submit: () => void;
    buttonRenderer: (toggle: () => void) => React.ReactNode,
}

interface IConfirmWindowState {
    modal: boolean;
}

export class ConfirmWindow extends React.Component<IConfirmWindowProps> {

    public state: IConfirmWindowState;

    constructor(props: IConfirmWindowProps) {
        super(props);
        this.state = {
            modal: false,
        };
        this.toggle = this.toggle.bind(this);
    }

    public toggle() {
        this.setState({
            modal: !this.state.modal
        });
    }

    public render() {
        const { submit, text, title, buttonRenderer } = this.props;

        const realSubmit = (e: any) => {
            submit();
            this.toggle();
            e.preventDefault();
        };

        return (
            <React.Fragment>
                {buttonRenderer(this.toggle)}
                <Modal isOpen={this.state.modal} toggle={this.toggle}>
                    <ModalHeader toggle={this.toggle}>{title}</ModalHeader>
                    <ModalBody>
                        {text}
                        <Form onSubmit={realSubmit} className="mt-1">
                            <Button color="primary" type="submit">OK</Button>
                            {' '}
                            <Button color="secondary" onClick={this.toggle}>Cancel</Button>
                        </Form>
                    </ModalBody>
                </Modal>
            </React.Fragment>
        );
    }
}

