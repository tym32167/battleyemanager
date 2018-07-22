import { userConstants } from '../constants';
import { userService } from '../services';

export const userActions = {
    getUsers,
    getUser
};

function getUsers() {
    return dispatch => {
        dispatch(request([]));
        userService.getUsers()
            .then(
                users => {
                    dispatch(success(users));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(users) { return { type: userConstants.USERS_REQUEST, users } }
    function success(users) { return { type: userConstants.USERS_SUCCESS, users } }
    function failure(error) { return { type: userConstants.USERS_FAILURE, error } }
}

function getUser(id) {
    return dispatch => {
        dispatch(request({id}));
        userService.getUser(id)
            .then(
                user => {
                    dispatch(success(user));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(user) { return { type: userConstants.USER_EDIT_REQUEST, user } }
    function success(user) { return { type: userConstants.USER_EDIT_SUCCESS, user } }
    function failure(error) { return { type: userConstants.USER_EDIT_FAILURE, error } }
}