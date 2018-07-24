import {
    serverConstants
} from '../constants';

export function serversReducer(
    state = {
        items: [],        
    }, action) {    

    switch (action.type) {
        case serverConstants.GET_SERVERS_REQUEST:
            return {
                ...state,
                error: ''
            };
        case serverConstants.GET_SERVERS_SUCCESS:
            return {
                items: [...action.items]
            };
        case serverConstants.GET_SERVERS_FAILURE:
            return {                
                error: action.error
            };
        default:
            return state
    }
}