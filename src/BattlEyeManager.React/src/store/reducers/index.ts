import {combineReducers} from 'redux';
import {reducer as formReducer} from 'redux-form';
import {authentication} from './auth';
import { currentUsersReducer } from './currentUser';
<<<<<<< HEAD:src/BattlEyeManager.React/src/store/reducers/index.js
import { kickReasonsReducer } from './kickReason';
import { banReasonsReducer } from './banReason';
=======
import { onlineChatReducer } from './onlineChat';
import { onlinePlayersReducer } from './onlinePlayer';
import { onlineServersReducer } from './onlineServer';
import {serversReducer} from "./server";
import {usersReducer} from "./user";
>>>>>>> feature/react_typescript:src/BattlEyeManager.React/src/store/reducers/index.ts

const appReducer = combineReducers({
    auth: authentication,        
    currentUser: currentUsersReducer,
    form: formReducer,    
    onlineChat: onlineChatReducer,
<<<<<<< HEAD:src/BattlEyeManager.React/src/store/reducers/index.js
    currentUser: currentUsersReducer,
    kickReasons: kickReasonsReducer,
    banReasons: banReasonsReducer
=======
    onlinePlayers: onlinePlayersReducer,
    onlineServers: onlineServersReducer,
    servers: serversReducer,
    users: usersReducer,    
>>>>>>> feature/react_typescript:src/BattlEyeManager.React/src/store/reducers/index.ts
});

export default appReducer;