import { authConstants } from '../constants';
import { authService, history } from '../services';

export const authActions = {
    login,
    logout
};

function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        authService.login(username, password)
            .then(
                user => { 
                    dispatch(success(user));                    
                    history.push('/');
                },
                error => {
                    dispatch(failure(error));
                    //dispatch(alertActions.error(error));
                }
            );
    };

    function request(user) { return { type: authConstants.LOGIN_REQUEST, user } }
    function success(user) { return { type: authConstants.LOGIN_SUCCESS, user } }
    function failure(error) { return { type: authConstants.LOGIN_FAILURE, error } }
}

function logout() {
    authService.logout();
    history.push('/');
    return { type: authConstants.LOGOUT };
}
