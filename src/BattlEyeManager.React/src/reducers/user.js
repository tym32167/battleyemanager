import { userConstants } from '../constants';

export function usersReducer(state = {users:[]}, action) {
    switch (action.type) {
        case userConstants.GET_USERS_REQUEST:
            return {
                ...state,
                error : '',
                users: [...action.users]
            };
        case userConstants.GET_USERS_SUCCESS:
            return {
                ...state,
                error : '',
                users: [...action.users]
            };
        case userConstants.GET_USERS_FAILURE:
            return {
                ...state,
                error: action.error
            };
        default:
            return state
    }
}

export function userReducer(state = {}, action){
    switch (action.type) {
        case userConstants.GET_USER_BY_ID_REQUEST:
            return {
                request: {...action.user}
            };
        case userConstants.GET_USER_BY_ID_SUCCESS:
            return {
                user: {...action.user}
            };
        case userConstants.GET_USER_BY_ID_FAILURE:
            return {
                ...state,
                error: action.error
            };
        default:
            return state
    }
}