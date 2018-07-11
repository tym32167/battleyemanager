import {authentication} from './auth';
import {combineReducers} from 'redux';


const appReducer = combineReducers({
  auth: authentication
});

export default appReducer;