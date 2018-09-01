import { commonService } from './commonService';
const baseUrl = '/api/currentuser/';

export const currentUserService = {
    getItem: (id: any) => commonService.getItem(baseUrl, id)
};
