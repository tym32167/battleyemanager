import { IOnlinePlayer } from 'src/models';
import { ReadonlyCommonService } from './core/readonlycommonservice';

const baseUrl = '/api/onlineserver/'

const service = new ReadonlyCommonService<IOnlinePlayer>();

export const onlinePlayersService = {
    getItems: (serverId: string | number) => service.getItemsBy(baseUrl + serverId + '/players/'),
};
