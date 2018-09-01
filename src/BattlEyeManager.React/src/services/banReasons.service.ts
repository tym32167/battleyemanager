import { CommonService, IService } from '.';
import { IBanReason } from '../models';

const baseUrl = '/api/banreason/'
export const banReasonsService: IService<IBanReason> = new CommonService<IBanReason>(baseUrl);
