import React from 'react';
import { Router, Route, Redirect } from "react-router-dom";
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Test, UserList, Login, Home } from './pages';
import {history}  from './services/history';
import {PageTemplate} from './shared_components';

    const App = () => <div className="App">
      <Router history={history}>
        <div>
          <DefaultLayout exact path="/" component={Home} />
          <DefaultLayout exact path="/users" component={UserList} />
          <DefaultLayout exact path="/test" component={Test} />

          <Route exact path="/login" component={Login} />
        </div>
      </Router>
    </div>;

export default App;

const DefaultLayout = ({component: Component, ...rest}) => {
  return (
    <Route {...rest} render={matchProps => (
      <PageTemplate>
          <Component {...matchProps} />
      </PageTemplate>
    )} />
  )
};

const PrivateRoute = ({ component: Component, ...rest }) => (
  <Route {...rest} render={props => (
    localStorage.getItem('user')
      ? <Component {...props} />
      : <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
  )} />
)
