import { Home } from './pages/home'
import { Users } from './pages/users'
import React, { Component } from 'react';
import { BrowserRouter as Router, Route } from "react-router-dom";
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Test } from './pages/test';

class App extends Component {
  render() {
    return (    
      <div className="App">
        <Router>
          <div>
            <Route exact path="/" component={Home} />
            <Route exact path="/users" component={Users} />
            <Route exact path="/test" component={Test} />
          </div>
        </Router>
      </div>
    );
  }
}

export default App;

