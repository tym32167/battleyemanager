import { IIdentity } from '../models/iidentity';
import { commonService } from './commonService';

const baseUrl = '/api/user/';

export const userService = {
    addItem: <T>(item: T) => commonService.addItem(baseUrl, item),
    deleteItem: (id: string | number) => commonService.deleteItem(baseUrl, id),
    getItem: <T>(id: string | number) => commonService.getItem<T>(baseUrl, id),
    getItems: <T>() => commonService.getItems<T>(baseUrl),
    updateItem: <T extends IIdentity>(item: T) => commonService.updateItem(baseUrl, item),
};

