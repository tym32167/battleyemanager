import {authentication} from './auth';
import {usersReducer, userReducer} from "./user";
import {combineReducers} from 'redux';
import {reducer as formReducer} from 'redux-form';

const appReducer = combineReducers({
    auth: authentication,
    user: userReducer,
    users: usersReducer,
    form: formReducer,
});

export default appReducer;