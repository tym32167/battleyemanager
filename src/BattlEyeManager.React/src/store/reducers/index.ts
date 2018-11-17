import { combineReducers } from 'redux';
import { reducer as formReducer } from 'redux-form';
import { authentication } from './auth';
import { banReasonsReducer } from './banReason';
import { currentUsersReducer } from './currentUser';
import { kickReasonsReducer } from './kickReason';
import { onlineBansReducer } from './onlineBan';
import { onlineChatReducer } from './onlineChat';
import { onlinePlayersReducer } from './onlinePlayer';
import { onlineServersReducer } from './onlineServer';
import { serversReducer } from "./server";
import { usersReducer } from "./user";


const appReducer = combineReducers({
    auth: authentication,
    banReasons: banReasonsReducer,
    currentUser: currentUsersReducer,
    form: formReducer,
    kickReasons: kickReasonsReducer,
    onlineBans: onlineBansReducer,
    onlineChat: onlineChatReducer,
    onlinePlayers: onlinePlayersReducer,
    onlineServers: onlineServersReducer,
    servers: serversReducer,
    users: usersReducer,
});

export default appReducer;