import {authentication} from './auth';
import {usersReducer} from "./user";
import {serversReducer} from "./server";
import {combineReducers} from 'redux';
import {reducer as formReducer} from 'redux-form';
import { onlineServersReducer } from './onlineServer';
import { onlinePlayersReducer } from './onlinePlayer';

const appReducer = combineReducers({
    auth: authentication,    
    users: usersReducer,
    servers: serversReducer,
    onlineServers: onlineServersReducer,
    form: formReducer,
    onlinePlayers: onlinePlayersReducer
});

export default appReducer;