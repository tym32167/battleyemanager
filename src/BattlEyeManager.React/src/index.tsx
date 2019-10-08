import 'core-js';
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './i18n';
import './index.css';
import registerServiceWorker from './registerServiceWorker';

ReactDOM.render(
  <App />
  , document.getElementById('root'));
registerServiceWorker();
