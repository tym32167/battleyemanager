import {
    userConstants
} from '../constants';

export function usersReducer(
    state = {
        users: [],        
    }, action) {

    state = userReducer(state, action);

    switch (action.type) {
        case userConstants.GET_USERS_REQUEST:
            return {
                ...state,
                error: ''
            };
        case userConstants.GET_USERS_SUCCESS:
            return {
                users: [...action.users]
            };
        case userConstants.GET_USERS_FAILURE:
            return {                
                error: action.error
            };
        default:
            return state
    }
}

export function userReducer(state = {}, action) {
    switch (action.type) {
        case userConstants.GET_USER_BY_ID_REQUEST:
            return {                
                request: { ...action.user }
            };
        case userConstants.GET_USER_BY_ID_SUCCESS:
            return {                
                user: { ...action.user }
            };
        case userConstants.GET_USER_BY_ID_FAILURE:
            return {                
                error: action.error
            };

        case userConstants.UPDATE_USER_REQUEST:
            return {
                ...state,
                updateRequest: { ...action.user }
            };
        case userConstants.UPDATE_USER_SUCCESS:
            return {
                updateUser: { ...action.user }
            };
        case userConstants.UPDATE_USER_FAILURE:
            return {
                ...state,
                error: action.error
            };

        case userConstants.CREATE_USER_REQUEST:
            return {
                ...state,
                createRequest: { ...action.user }
            };
        case userConstants.CREATE_USER_SUCCESS:
            return {
                createUser: { ...action.user }
            };
        case userConstants.CREATE_USER_FAILURE:
            return {
                ...state,
                error: action.error
            };


        case userConstants.DELETE_USER_REQUEST:
            return {
                ...state,
                deleteRequest: { ...action.user }
            };
        case userConstants.DELETE_USER_SUCCESS:
            return {
                ...state
            };
        case userConstants.DELETE_USER_FAILURE:
            return {
                ...state,
                error: action.error
            };

        default:
            return state
    }
}