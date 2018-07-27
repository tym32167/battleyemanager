import axios from 'axios';

export const commonService = {
    getItems,
    getItem,
    updateItem,
    addItem,
    deleteItem
};

function getItems(baseUrl) {
    return axios.get(baseUrl)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getItem(baseUrl, id) {
    return axios.get(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function updateItem(baseUrl, item) {
    return axios.post(baseUrl + item.id, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function addItem(baseUrl, item) {
    return axios.put(baseUrl, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function deleteItem(baseUrl, id) {
    return axios.delete(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getError(error){
    if (error.response && error.response.data) return error.response.data;
    return error.message;
}