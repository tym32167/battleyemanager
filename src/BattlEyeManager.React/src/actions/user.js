import { userConstants } from '../constants';
import { userService } from '../services';

export const userActions = {
    getUsers
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
