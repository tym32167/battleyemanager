import {authentication} from './auth';
import {usersReducer} from "./user";
import {combineReducers} from 'redux';
import {reducer as formReducer} from 'redux-form';

const appReducer = combineReducers({
    auth: authentication,    
    users: usersReducer,
    form: formReducer,
});

export default appReducer;