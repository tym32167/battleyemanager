import { commonService } from './commonService';

const baseUrl = '/api/onlineserver/'

export const onlineServerService = {
    getItem: (id: any) => commonService.getItem(baseUrl, id),
    getItems: () => commonService.getItems(baseUrl),
};

