import {authentication} from './auth';
import {usersReducer} from "./user";
import {serversReducer} from "./server";
import {combineReducers} from 'redux';
import {reducer as formReducer} from 'redux-form';

const appReducer = combineReducers({
    auth: authentication,    
    users: usersReducer,
    servers: serversReducer,
    form: formReducer,
});

export default appReducer;