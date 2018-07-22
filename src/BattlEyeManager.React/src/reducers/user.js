import { userConstants } from '../constants';

export function userReducer(state = {users:[], userEdit:{}}, action) {
    state.userEdit = userEditReducer(state.userEdit, action);
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

function userEditReducer(state = {}, action){
    switch (action.type) {
        case userConstants.GET_USER_BY_ID_REQUEST:
            return {
                ...state,
                error : '',
                user: [...action.user]
            };
        case userConstants.GET_USER_BY_ID_SUCCESS:
            return {
                ...state,
                error : '',
                user: [...action.user]
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