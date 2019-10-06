import axios from 'axios';
import { IServerStatsModel } from "src/models";

const baseUrl = '/api/ServerStats/';

const getError = (error: any) => {
    if (error.response && error.response.data) { return error.response.data; }
    return error.message;
}

export const serverStatsService = {
    getStats: () => {
        return axios.get<IServerStatsModel>(baseUrl)
            .then(response => response.data)
            .catch(error => Promise.reject(getError(error)));
    },
};

