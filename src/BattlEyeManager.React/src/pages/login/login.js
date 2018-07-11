import React, { Component } from 'react';
import './login.css'
import { login } from '../../actions/auth'


export class Login extends Component {

  constructor(props) {
    super(props);

    this.state = {
      username: '',
      password: '',
      submitted: false
    };
    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(e) {
    const { name, value } = e.target;
    this.setState({ [name]: value });
  }

  handleSubmit(e) {
    e.preventDefault();

    this.setState({ submitted: true });
    const { username, password } = this.state;
    const { dispatch } = this.props;
    if (username && password) {
      dispatch(login(username, password));
    }
  }

  render() {

    const { username, password, submitted } = this.state;

    return (
      <div className="text-center body">
        <form className="form-signin" onSubmit={this.handleSubmit} >
          <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
          <div className="form-group">
            <input type="text" className="form-control" name="username" placeholder="User Name" value={username} onChange={this.handleChange} required />
          </div>

          <div className="form-group">
            <input type="password" className="form-control" name="password" placeholder="Password" value={password} onChange={this.handleChange} required />
          </div>

          <button className="btn btn-lg btn-primary btn-block">Sign in</button>
          <hr />
          <footer>
            <p>&copy; 2018 - BattlEye Manager</p>
          </footer>
        </form>
      </div>);
  }
}
