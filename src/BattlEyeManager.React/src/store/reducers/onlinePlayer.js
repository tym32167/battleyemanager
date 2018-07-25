import {
    onlinePlayerConstants
} from '../constants';

export function onlinePlayersReducer(
    state = {        
    }, action) {

    switch (action.type) {
        case onlinePlayerConstants.GET_ONLINE_PLAYERS_REQUEST:
            return {
                ...state,  
                [action.serverId] : undefined              
            };
        case onlinePlayerConstants.GET_ONLINE_PLAYERS_SUCCESS:
            return {
                ...state,
                [action.serverId] : { items: action.items }
            };
        case onlinePlayerConstants.GET_ONLINE_PLAYERS_FAILURE:
            return {
                ...state,
                [action.serverId] : { items: action.error }
            };
        default:
            return state
    }
}
