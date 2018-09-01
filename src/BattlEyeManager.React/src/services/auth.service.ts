import axios from 'axios';

export const authService = {
    login,
    logout
};

function login(username: string, password: string) {
    return axios.post('/api/account/auth', { username, password })
        .then(response => response.data)
        .catch(error => Promise.reject(error.message));
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}
