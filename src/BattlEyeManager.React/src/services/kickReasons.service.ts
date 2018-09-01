import { CommonService, IService } from '.';
import { IKickReason } from '../models';

const baseUrl = '/api/kickreason/'
export const kickReasonsService: IService<IKickReason> = new CommonService<IKickReason>(baseUrl);

