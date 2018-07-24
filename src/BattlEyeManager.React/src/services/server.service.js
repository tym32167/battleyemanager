import axios from 'axios';

const baseUrl = '/api/server/'

export const serverService = {
    getAll,
    get,
    update,
    add,
    del
};

function getAll() {
    return axios.get(baseUrl)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function get(id) {
    return axios.get(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function update(item) {
    return axios.post(baseUrl + item.id, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function add(item) {
    return axios.put(baseUrl, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function del(id) {
    return axios.delete(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getError(error){
    if (error.response && error.response.data) return error.response.data;
    return error.message;
}