import { IUser } from '../models';
import { commonService } from './commonService';
import { IReadonlyService } from './models';
const baseUrl = '/api/currentuser/';

export const currentUserService: IReadonlyService<IUser> = {
    getItem: (id: string | number) => commonService.getItem<IUser>(baseUrl, id),
    getItems: () => new Promise<IUser[]>(_ => []),
};


