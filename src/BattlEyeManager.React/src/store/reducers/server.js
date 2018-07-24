import {
    serverConstants
} from '../constants';

export function serversReducer(
    state = {
        items: [],
    }, action) {

    state = serverReducer(state, action);

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


export function serverReducer(state = {}, action) {
    switch (action.type) {


        case serverConstants.CREATE_SERVER_REQUEST:
            return {
                ...state,
                createRequest: { ...action.item
                }
            };
        case serverConstants.CREATE_SERVER_SUCCESS:
            return {
                createItem: { ...action.item
                }
            };
        case serverConstants.CREATE_SERVER_FAILURE:
            return {
                ...state,
                error: action.error
            };

        default:
            return state
    }
}