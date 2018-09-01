import { commonService } from './commonService';

const baseUrl = '/api/onlineserver/'

export const onlineChatService = {
    addItem: (serverId: any, item: any) => commonService.addItem(baseUrl + serverId + '/chat/', item),
    getItems: (serverId: any) => commonService.getItems(baseUrl + serverId + '/chat/'),
};
