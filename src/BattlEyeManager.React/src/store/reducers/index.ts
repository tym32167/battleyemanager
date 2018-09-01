import {combineReducers} from 'redux';
import {reducer as formReducer} from 'redux-form';
import {authentication} from './auth';
import { currentUsersReducer } from './currentUser';
import { onlineChatReducer } from './onlineChat';
import { onlinePlayersReducer } from './onlinePlayer';
import { onlineServersReducer } from './onlineServer';
import {serversReducer} from "./server";
import {usersReducer} from "./user";

const appReducer = combineReducers({
    auth: authentication,        
    currentUser: currentUsersReducer,
    form: formReducer,    
    onlineChat: onlineChatReducer,
    onlinePlayers: onlinePlayersReducer,
    onlineServers: onlineServersReducer,
    servers: serversReducer,
    users: usersReducer,    
});

export default appReducer;