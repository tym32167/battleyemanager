import axios from 'axios';
import { IAuthUserInfo } from 'src/models';

export const authService = {
    login,
    logout
};

function login(username: string, password: string) {
    return axios.post<IAuthUserInfo>('/api/account/auth', { username, password })
        .then(response => response.data)
        .catch(error => Promise.reject(error.message));
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}
