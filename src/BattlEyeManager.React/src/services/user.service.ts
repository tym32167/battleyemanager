import { commonService } from './commonService';

const baseUrl = '/api/user/';

export const userService = {
    addItem: (item: any) => commonService.addItem(baseUrl, item),
    deleteItem: (id: any) => commonService.deleteItem(baseUrl, id),
    getItem: (id: any) => commonService.getItem(baseUrl, id),
    getItems: () => commonService.getItems(baseUrl),
    updateItem: (item: any) => commonService.updateItem(baseUrl, item),
};

