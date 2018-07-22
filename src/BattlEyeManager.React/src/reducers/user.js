import { userConstants } from '../constants';

export function userReducer(state = [], action) {
    switch (action.type) {
        case userConstants.USERS_REQUEST:
            return {
                users: [...action.users]
            };
        case userConstants.USERS_SUCCESS:
            return {
                users: [...action.users]
            };
        case userConstants.USERS_FAILURE:
            return {};
        default:
            return state
    }
}