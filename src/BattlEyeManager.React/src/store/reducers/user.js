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
                ...state,
                user: undefined,
                request: {...action.user}
            };
        case userConstants.GET_USER_BY_ID_SUCCESS:
            return {
                ...state,
                user: {...action.user}
            };
        case userConstants.GET_USER_BY_ID_FAILURE:
            return {
                ...state,
                error: action.error
            };

        case userConstants.UPDATE_USER_REQUEST:
            return {
                ...state,
                updateRequest: {...action.user}
            };
        case userConstants.UPDATE_USER_SUCCESS:
            return {
                updateUser: {...action.user}
            };
        case userConstants.UPDATE_USER_FAILURE:
            return {
                ...state,
                updateError: action.error
            };

        default:
            return state
    }
}