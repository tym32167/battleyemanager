import axios from 'axios';
import { ILineGraphModel } from "src/models";

const baseUrl = '/api/ServerStats/';

const getError = (error: any) => {
    if (error.response && error.response.data) { return error.response.data; }
    return error.message;
}

export const serverStatsService = {
    getStatsLastWeek: () => {
        return axios.get<ILineGraphModel>(baseUrl + 'LastWeek/')
            .then(response => response.data)
            .catch(error => Promise.reject(getError(error)));
    },

    getStatsLastDay: () => {
        return axios.get<ILineGraphModel>(baseUrl + 'LastDay/')
            .then(response => response.data)
            .catch(error => Promise.reject(getError(error)));
    },
};

