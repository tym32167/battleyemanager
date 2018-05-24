import React, { Component } from 'react';
import './login.css'

export class Login extends Component {

  render() {
    const { handleSubmit } = this.props;

    return (
      <div className="text-center body">
        <form className="form-signin" >
          <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
          <div className="form-group">
            <input type="text" className="form-control" name="email" placeholder="User Name" required autofocus />
          </div>

          <div className="form-group">
            <input type="password" className="form-control" name="password" placeholder="Password" required />
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
