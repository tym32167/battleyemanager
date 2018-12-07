import { IOnlineServer } from 'src/models';
import { IReadonlyService } from './core';
import { ReadonlyCommonService } from './core/readonlycommonservice';

const baseUrl = '/api/onlineserver/'
export const onlineServerService: IReadonlyService<IOnlineServer>
    = new ReadonlyCommonService<IOnlineServer>(baseUrl);

