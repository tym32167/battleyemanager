import { Home } from './pages/home'
import { Users } from './pages/users'
import React, { Component } from 'react';
import { createStore, applyMiddleware } from 'redux';
import { BrowserRouter as Router, Route } from "react-router-dom";
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Test } from './pages/test';
import { Login } from './pages/login/login';
import reducers from './reducers';
import reduxThunk from 'redux-thunk';

const createStoreWithMiddleware = applyMiddleware(reduxThunk)(createStore);
const store = createStoreWithMiddleware(reducers);

class App extends Component {
  render() {
    return (    
      <div className="App">
        <Router>
          <div>
            <Route exact path="/" component={Home} />
            <Route exact path="/users" component={Users} />
            <Route exact path="/test" component={Test} />
            <Route exact path="/login" component={Login} />
          </div>
        </Router>        
      </div>
    );
  }
}

export default App;

