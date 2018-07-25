import React from 'react';
import { Router, Route, Redirect } from "react-router-dom";
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Test, Users, Login, Home, Servers } from './pages';
import { history } from './services/history';
import { PageTemplate } from './shared_components';
import PropTypes from 'prop-types';


const App = () => <div className="App">
  <Router history={history}>
    <div>
      <DefaultLayout exact path="/" component={Home} />
      <DefaultLayout path="/users" component={Users} />
      <DefaultLayout path="/servers" component={Servers} />
      <DefaultLayout exact path="/test" component={Test} />
      <Route exact path="/login" component={Login} />
    </div>
  </Router>
</div>;

export default App;

const DefaultLayout = ({ component: Component, ...rest }) => {
  return (
    <Route {...rest} render={matchProps => (

      localStorage.getItem('user')
        ?
        <PageTemplate>
          <Component {...matchProps} />
        </PageTemplate>
        : 
        <Redirect to={{ pathname: '/login', state: { from: matchProps.location } }} />
    )} />
  )
};

DefaultLayout.propTypes = {
  component : PropTypes.instanceOf(React.Component)
}