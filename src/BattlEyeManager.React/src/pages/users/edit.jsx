import React, { Component } from 'react';
import { userService } from '../../services';
import { Link } from 'react-router-dom';
import { Field, reduxForm } from 'redux-form'


export class Edit extends Component {

    constructor(props) {
        super(props);
        this.state = {
            id: props.match.params.id
        };
        this.fetchData = this.fetchData.bind(this);
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData() {
        var id = this.state.id;
        userService.getUser(id)
            .then(data => {
                this.setState({ user: data });
            });
    }

    submit = values => {
        // print the form values to the console
        console.log(values)
      }

    render() {
        return (
            <div className="my-3 p-3 bg-white rounded box-shadow">
                <h2>Edit User</h2>
                <Link to="/users">Back to list</Link>
                <EditUserForm onSubmit={this.submit} />
            </div>
        );
    }
}

let EditUserForm = props => {
    const { handleSubmit } = props
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="firstName">First Name</label>
                <Field name="firstName" component="input" type="text" />
            </div>
            <div>
                <label htmlFor="lastName">Last Name</label>
                <Field name="lastName" component="input" type="text" />
            </div>
            <div>
                <label htmlFor="email">Email</label>
                <Field name="email" component="input" type="email" />
            </div>
            <button type="submit">Submit</button>
        </form>
    );
}

EditUserForm = reduxForm({
    // a unique name for the form
    form: 'EditUserForm'
})(EditUserForm)