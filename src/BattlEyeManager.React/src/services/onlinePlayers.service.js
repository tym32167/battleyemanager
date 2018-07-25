import axios from 'axios';

const baseUrl = '/api/onlineserver/'

export const onlinePlayersService = {
    getAll
};

function getAll(serverId) {
    return axios.get(baseUrl + serverId + '/players/')
        .then(response => response.data)
        .catch(error => Promise.reject(getError(error)));
}

function getError(error){
    if (error.response && error.response.data) return error.response.data;
    return error.message;
}