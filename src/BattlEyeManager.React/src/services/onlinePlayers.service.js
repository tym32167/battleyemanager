import { commonService } from './commonService';

const baseUrl = '/api/onlineserver/'

export const onlinePlayersService = {
    getItems : (serverId) => commonService.getItems(baseUrl + serverId + '/players/'),
};
