import { userConstants } from '../constants';
import {history, userService} from '../../services';

export const userActions = {
    getUsers,
    getUser,
    updateUser,
    addUser
};

function addUser(user) {
    return dispatch => {
        dispatch(request(user));
        userService.addUser(user)
            .then(
                user => {
                    dispatch(success(user));
                    history.push('/users');
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(user)  { return { type: userConstants.CREATE_USER_REQUEST, user } }
    function success(user)  { return { type: userConstants.CREATE_USER_SUCCESS, user } }
    function failure(error) { return { type: userConstants.CREATE_USER_FAILURE, error } }
}

function updateUser(user) {
    return dispatch => {
        dispatch(request(user));
        userService.updateUser(user)
            .then(
                user => {
                    dispatch(success(user));
                    history.push('/users');
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(user) { return { type: userConstants.UPDATE_USER_REQUEST, user } }
    function success(user) { return { type: userConstants.UPDATE_USER_SUCCESS, user } }
    function failure(error) { return { type: userConstants.UPDATE_USER_FAILURE, error } }
}




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
    function request(users) { return { type: userConstants.GET_USERS_REQUEST, users } }
    function success(users) { return { type: userConstants.GET_USERS_SUCCESS, users } }
    function failure(error) { return { type: userConstants.GET_USERS_FAILURE, error } }
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
    function request(user) { return { type: userConstants.GET_USER_BY_ID_REQUEST, user } }
    function success(user) { return { type: userConstants.GET_USER_BY_ID_SUCCESS, user } }
    function failure(error) { return { type: userConstants.GET_USER_BY_ID_FAILURE, error } }
}