import { IKickReason } from '../models';
import { IService } from './core';
import { CommonService } from './core/commonService';

const baseUrl = '/api/kickreason/'
export const kickReasonsService: IService<IKickReason> = new CommonService<IKickReason>(baseUrl);

