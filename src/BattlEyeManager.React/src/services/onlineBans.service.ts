import { IOnlineBan } from 'src/models';
import { ReadonlyCommonService } from './core/readonlycommonservice';

const baseUrl = '/api/onlineserver/'

const service = new ReadonlyCommonService<IOnlineBan>();

export const onlineBansService = {
    getItems: (serverId: string | number) => service.getItemsBy(baseUrl + serverId + '/bans/'),
};
