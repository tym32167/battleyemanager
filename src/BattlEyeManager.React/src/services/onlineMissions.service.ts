import { IOnlineMission } from 'src/models';
import { ReadonlyCommonService } from './core/readonlycommonservice';

const baseUrl = '/api/onlineserver/'

const service = new ReadonlyCommonService<IOnlineMission>();

export const onlineMissionsService = {
    getItems: (serverId: string | number) => service.getItemsBy(baseUrl + serverId + '/missions/'),
};
