import axios from 'axios';

export const commonService = {
    addItem,
    deleteItem,
    getItem,
    getItems,
    updateItem,
};

function getItems(baseUrl: string) {
    return axios.get(baseUrl)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getItem(baseUrl: string, id: any) {
    return axios.get(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function updateItem(baseUrl: string, item: any) {
    return axios.post(baseUrl + item.id, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function addItem(baseUrl: string, item: any) {
    return axios.put(baseUrl, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function deleteItem(baseUrl: string, id: any) {
    return axios.delete(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getError(error: any) {
    if (error.response && error.response.data) { return error.response.data; }
    return error.message;
}