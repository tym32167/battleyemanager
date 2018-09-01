import { IUser } from '../models';
import { IService } from './core';
import { CommonService } from './core/commonService';

const baseUrl = '/api/user/';
export const userService: IService<IUser> = new CommonService<IUser>(baseUrl);