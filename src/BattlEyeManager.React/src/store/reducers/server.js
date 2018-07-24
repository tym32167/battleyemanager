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


        case serverConstants.GET_SERVER_BY_ID_REQUEST:
            return {                
                request: { ...action.item }
            };
        case serverConstants.GET_SERVER_BY_ID_SUCCESS:
            return {                
                item: { ...action.item }
            };
        case serverConstants.GET_SERVER_BY_ID_FAILURE:
            return {                
                error: action.error
            };

        case serverConstants.UPDATE_SERVER_REQUEST:
            return {
                ...state,
                updateRequest: { ...action.item }
            };
        case serverConstants.UPDATE_SERVER_SUCCESS:
            return {
                updateUser: { ...action.item }
            };
        case serverConstants.UPDATE_SERVER_FAILURE:
            return {
                ...state,
                error: action.error
            };       


        case serverConstants.DELETE_SERVER_REQUEST:
            return {
                ...state,
                deleteRequest: { ...action.item }
            };
        case serverConstants.DELETE_SERVER_SUCCESS:
            return {
                ...state
            };
        case serverConstants.DELETE_SERVER_FAILURE:
            return {
                ...state,
                error: action.error
            };

        default:
            return state
    }
}