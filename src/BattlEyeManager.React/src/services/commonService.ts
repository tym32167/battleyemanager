import axios from 'axios';
import { IIdentity } from '../models/iidentity';

export const commonService = {
    addItem,
    deleteItem,
    getItem,
    getItems,
    updateItem,
};

function getItems<T>(baseUrl: string) {
    return axios.get<T[]>(baseUrl)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getItem<T>(baseUrl: string, id: string | number) {
    return axios.get<T>(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function updateItem<T extends IIdentity>(baseUrl: string, item: T) {
    return axios.post<T>(baseUrl + item.id, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function addItem<T>(baseUrl: string, item: T) {
    return axios.put<T>(baseUrl, item)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function deleteItem(baseUrl: string, id: string | number) {
    return axios.delete(baseUrl + id)
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getError(error: any) {
    if (error.response && error.response.data) { return error.response.data; }
    return error.message;
}