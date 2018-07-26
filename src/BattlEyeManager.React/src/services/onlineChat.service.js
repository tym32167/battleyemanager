import { commonService } from './commonService';

const baseUrl = '/api/onlineserver/'

export const onlineChatService = {
    getItems : (serverId) => commonService.getItems(baseUrl + serverId + '/chat/'),
};
