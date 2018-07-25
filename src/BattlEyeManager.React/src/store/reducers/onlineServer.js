import {
    onlineServerConstants
} from '../constants';

export function onlineServersReducer(
    state = {
        items: [],
    }, action) {

    state = onlineServerReducer(state, action);

    switch (action.type) {
        case onlineServerConstants.GET_ONLINE_SERVERS_REQUEST:
            return {
                ...state,
                error: ''
            };
        case onlineServerConstants.GET_ONLINE_SERVERS_SUCCESS:
            return {
                items: [...action.items]
            };
        case onlineServerConstants.GET_ONLINE_SERVERS_FAILURE:
            return {
                error: action.error
            };
        default:
            return state
    }
}

export function onlineServerReducer(state = {}, action) {
    switch (action.type) {

        case onlineServerConstants.GET_ONLINE_SERVER_BY_ID_REQUEST:
            return {
                request: { ...action.item
                }
            };
        case onlineServerConstants.GET_ONLINE_SERVER_BY_ID_SUCCESS:
            return {
                item: { ...action.item
                }
            };
        case onlineServerConstants.GET_ONLINE_SERVER_BY_ID_FAILURE:
            return {
                error: action.error
            };

        default:
            return state
    }
}