import { authConstants } from '../constants';
import { authService, history } from '../../services';

export const authActions = {
    login,
    logout
};

function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        authService.login(username, password)
            .then(
                response => { 
                    let user = response;
                    localStorage.setItem('user', JSON.stringify(user));
                    dispatch(success(user));                    
                    history.push('/');
                }
            )
            .catch(error => dispatch(failure(error)));
    };

    function request(user) { return { type: authConstants.LOGIN_REQUEST, user } }
    function success(user) { return { type: authConstants.LOGIN_SUCCESS, user } }
    function failure(error) { return { type: authConstants.LOGIN_FAILURE, error } }
}

function logout() {
     // remove user from local storage to log user out
    localStorage.removeItem('user');
    authService.logout();
    history.push('/');
    return { type: authConstants.LOGOUT };
}
