import { commonService } from './commonService';

const baseUrl = '/api/onlineserver/'

export const onlinePlayersService = {
    getItems: (serverId: any) => commonService.getItems(baseUrl + serverId + '/players/'),
};
