import axios from 'axios';
import { IOnlineBan } from 'src/models';
import { ReadonlyCommonService } from './core/readonlycommonservice';

const baseUrl = '/api/onlineserver/'

const service = new ReadonlyCommonService<IOnlineBan>();

export const onlineBansService = {
    getItems: (serverId: string | number) => service.getItemsBy(baseUrl + serverId + '/bans/'),
    removeBan: (serverId: number, banNumber: number) => {
        return axios.delete(baseUrl + serverId + '/bans/' + banNumber)
            .then(response => response.data)
            .catch(error => Promise.reject(service.getError(error)));
    }
};
