import { authService, history } from '../../services';
import { authConstants } from '../constants';

export const authActions = {
    login,
    logout
};

function login(username: string, password: string) {
    return (dispatch: any) => {
        dispatch(request({ username }));

        authService.login(username, password)
            .then(
                response => {
                    const user = response;
                    localStorage.setItem('user', JSON.stringify(user));
                    dispatch(success(user));
                    history.push('/');
                }
            )
            .catch(error => dispatch(failure(error)));
    };

    function request(user: any) { return { type: authConstants.LOGIN_REQUEST, user } }
    function success(user: any) { return { type: authConstants.LOGIN_SUCCESS, user } }
    function failure(error: any) { return { type: authConstants.LOGIN_FAILURE, error } }
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
    authService.logout();
    history.push('/');
    return { type: authConstants.LOGOUT };
}
