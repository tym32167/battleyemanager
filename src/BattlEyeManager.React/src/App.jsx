import React from 'react';
import { Router, Route, Redirect, Switch } from "react-router-dom";
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Test, Users, Login, Servers, OnlineServers, NotFound } from './pages';
import { history } from './services/history';
import { PageTemplate } from './shared_components';
import PropTypes from 'prop-types';

const App = () => <div className="App">
  <Router history={history}>
    <div>
      <Switch>
        <DefaultLayout exact path="/" component={OnlineServers} />
        <DefaultLayout path="/users" component={Users} />
        <DefaultLayout path="/servers" component={Servers} />
        <DefaultLayout path="/online" component={OnlineServers} />
        <DefaultLayout exact path="/test" component={Test} />
        <Route exact path="/login" component={Login} />

        <Route component={NotFound}/>
      </Switch>
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
  component: PropTypes.oneOfType([PropTypes.element, PropTypes.func]).isRequired
}