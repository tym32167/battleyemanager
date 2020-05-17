import axios from 'axios';
import { ISteamPlayersResponse } from "src/models";

const baseUrl = '/api/onlineserversteam/';

const getError = (error: any) => {
    if (error.response && error.response.data) { return error.response.data; }
    return error.message;
}

export const steamStatsService = {
    getPlayerStats: (serverId: number) => {
        return axios.get<ISteamPlayersResponse>(baseUrl + serverId + "/players")
            .then(response => response.data)
            .catch(error => Promise.reject(getError(error)));
    },
};