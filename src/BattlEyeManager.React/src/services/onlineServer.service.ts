import { IOnlineServer } from 'src/models';
import { commonService } from './commonService';
import { IReadonlyService } from './models';

const baseUrl = '/api/onlineserver/'

export const onlineServerService: IReadonlyService<IOnlineServer> = {
    getItem: (id: number | string) => commonService.getItem(baseUrl, id),
    getItems: () => commonService.getItems(baseUrl),
};

