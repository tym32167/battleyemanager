import {authentication} from './auth';
import {userReducer} from "./user";
import {combineReducers} from 'redux';
import {reducer as formReducer} from 'redux-form';

const appReducer = combineReducers({
    auth: authentication,
    user: userReducer,
    form: formReducer,
});

export default appReducer;