import { IBanReason } from '../models';
import { CommonService, IService } from './core';

const baseUrl = '/api/banreason/'
export const banReasonsService: IService<IBanReason> = new CommonService<IBanReason>(baseUrl);
