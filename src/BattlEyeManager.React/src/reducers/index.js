import {authentication} from './auth';
import {combineReducers} from 'redux';
import { reducer as formReducer } from 'redux-form';

const appReducer = combineReducers({
  auth: authentication,
  form: formReducer
});

export default appReducer;