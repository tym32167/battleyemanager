import {commonService} from './commonService';
const baseUrl = '/api/currentuser/';

export const currentUserService = {    
    getItem: (id) => commonService.getItem(baseUrl, id)
};
