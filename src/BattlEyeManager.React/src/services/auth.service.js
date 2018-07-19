import axios from 'axios';

export const authService = {
    login,
    logout
};

function login(username, password) {
    return axios.post('/api/account/auth', { username, password })
        .then(response => {             
            const user = response.data; 
            if (user && user.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
            }
            return user;
         })
         .catch(error => {             
             Promise.reject(error.message)
            });   
}

function logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('user');
}
