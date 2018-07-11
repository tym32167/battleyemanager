import { Home } from './pages/home'
import { Users } from './pages/users'
import React from 'react';
import { BrowserRouter as Router, Route, Redirect } from "react-router-dom";
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Test } from './pages/test';
import { Login } from './pages/login/login';

const App = () => <div className="App">
  <Router>
    <div>
      <PrivateRoute exact path="/" component={Home} />
      <PrivateRoute exact path="/users" component={Users} />      
      <PrivateRoute exact path="/test" component={Test} />
      <Route exact path="/login" component={Login} />
    </div>
  </Router>
</div>;

export default App;

const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route {...rest} render={props => (
      localStorage.getItem('user')
          ? <Component {...props} />
          : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
  )} />
)
