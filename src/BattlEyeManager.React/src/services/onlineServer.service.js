import { commonService } from './commonService';

const baseUrl = '/api/onlineserver/'

export const onlineServerService = {
    getItems : () => commonService.getItems(baseUrl),
    getItem: (id) => commonService.getItem(baseUrl, id)
};

