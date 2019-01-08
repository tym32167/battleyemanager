import axios from 'axios';
import { IOnlineMission } from 'src/models';
import { ReadonlyCommonService } from './core/readonlycommonservice';

const baseUrl = '/api/onlinemission/'

const service = new ReadonlyCommonService<IOnlineMission>();

export const onlineMissionsService = {
    getItems: (serverId: string | number) => service.getItemsBy(baseUrl + serverId + '/missions/'),

    setMission: (mission: IOnlineMission) => {
        return axios.post(baseUrl + mission.serverId + '/missions', mission)
            .then(response => response.data)
            .catch(error => Promise.reject(service.getError(error)));
    }
};


