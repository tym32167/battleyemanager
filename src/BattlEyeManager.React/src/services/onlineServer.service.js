import axios from 'axios';

const baseUrl = '/api/onlineserver/'

export const onlineServerService = {
    getAll,
    get
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

function getError(error){
    if (error.response && error.response.data) return error.response.data;
    return error.message;
}