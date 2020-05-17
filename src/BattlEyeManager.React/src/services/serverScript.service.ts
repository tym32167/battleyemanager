import axios from 'axios';
import { IServerScriptItem } from "src/models";
import { CommonService } from "./core/commonService";

const baseUrl = '/api/server/';

const service = new CommonService<IServerScriptItem>();

export const serverScriptService = {

    getItems: (serverId: number) => {
        return axios.get<IServerScriptItem[]>(baseUrl + serverId + "/scripts")
            .then(response => response.data)
            .catch(error => Promise.reject(service.getError(error)));
    },

    updateItem: (item: IServerScriptItem) => {
        return axios.post(baseUrl + item.serverId + "/scripts/" + item.id, item)
            .catch(error => Promise.reject(service.getError(error)));
    },

    addItem: (item: IServerScriptItem) => {
        return axios.put(baseUrl + item.serverId + "/scripts", item)
            .catch(error => Promise.reject(service.getError(error)));
    },

    deleteItem: (item: IServerScriptItem) => {
        return axios.delete(baseUrl + item.serverId + "/scripts/" + item.id)
            .catch(error => Promise.reject(service.getError(error)));
    },

    runItem: (item: IServerScriptItem) => {
        return axios.post(baseUrl + item.serverId + "/scripts/" + item.id + "/run")
            .catch(error => Promise.reject(service.getError(error)));
    },
};