import { commonService } from './commonService';

const baseUrl = '/api/kickreason/'

export const kickReasonsService = {
    getItems : () => commonService.getItems(baseUrl),
    getItem: (id) => commonService.getItem(baseUrl, id),
    updateItem: (item) => commonService.updateItem(baseUrl, item),
    addItem: (item) => commonService.addItem(baseUrl, item),
    deleteItem: (id) => commonService.deleteItem(baseUrl, id)
};

