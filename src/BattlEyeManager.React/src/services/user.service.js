import axios from 'axios';

export const userService = {
    getAll,
    get,
    update,
    add,
    del
};

function getAll() {
    return axios.get('/api/user')
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function get(id) {
    return axios.get('/api/user/' + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function update(user) {
    return axios.post('/api/user/' + user.id, user)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function add(user) {
    return axios.put('/api/user/', user)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function del(id) {
    return axios.delete('/api/user/' + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getError(error){
    if (error.response && error.response.data) return error.response.data;
    return error.message;
}