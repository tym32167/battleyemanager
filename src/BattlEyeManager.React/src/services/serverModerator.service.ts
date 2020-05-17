import axios from 'axios';
import { IServerModeratorItem } from "src/models";
import { CommonService } from "./core/commonService";

const baseUrl = '/api/servermoderator/';

const service = new CommonService<IServerModeratorItem>();

export const serverModeratorService = {

    getItems: (userId: string) => {
        return axios.get<IServerModeratorItem[]>(baseUrl + userId)
            .then(response => response.data)
            .catch(error => Promise.reject(service.getError(error)));
    },

    updateItems: (userId: string, data?: IServerModeratorItem[]) => {
        return axios.post<IServerModeratorItem[]>(baseUrl + userId, data)
            .then(response => response.data)
            .catch(error => Promise.reject(service.getError(error)));
    }
};