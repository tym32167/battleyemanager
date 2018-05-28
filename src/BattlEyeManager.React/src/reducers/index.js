import {logon} from './auth';
import {combineReducers} from 'redux';


const appReducer = combineReducers({
  auth: logon
});

export default appReducer;