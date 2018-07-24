import axios from 'axios';

export const userService = {
    getUsers,
    getUser,
    updateUser,
    addUser,
    deleteUser
};

function getUsers() {
    return axios.get('/api/user')
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getUser(id) {
    return axios.get('/api/user/' + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function updateUser(user) {
    return axios.post('/api/user/' + user.id, user)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function addUser(user) {
    return axios.put('/api/user/', user)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function deleteUser(id) {
    return axios.delete('/api/user/' + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getError(error){
    if (error.response && error.response.data) return error.response.data;
    return error.message;
}