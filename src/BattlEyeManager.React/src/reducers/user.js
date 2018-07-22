import { userConstants } from '../constants';

export function userReducer(state = {users:[], userEdit:{}}, action) {
    state.userEdit = userEditReducer(state.userEdit, action);
    switch (action.type) {
        case userConstants.USERS_REQUEST:
            return {
                ...state,
                error : '',
                users: [...action.users]
            };
        case userConstants.USERS_SUCCESS:
            return {
                ...state,
                error : '',
                users: [...action.users]
            };
        case userConstants.USERS_FAILURE:
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
        case userConstants.USER_EDIT_REQUEST:
            return {
                ...state,
                error : '',
                user: [...action.user]
            };
        case userConstants.USER_EDIT_SUCCESS:
            return {
                ...state,
                error : '',
                user: [...action.user]
            };
        case userConstants.USER_EDIT_FAILURE:
            return {
                ...state,
                error: action.error
            };
        default:
            return state
    }
}