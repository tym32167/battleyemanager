import axios from 'axios';
import { IOnlineServer } from 'src/models';
import { IReadonlyService } from '.';
import { ReadonlyCommonService } from './core/readonlycommonservice';




const baseUrl = '/api/onlineserver/'
export const onlineServerService: IReadonlyService<IOnlineServer>
    = new ReadonlyCommonService<IOnlineServer>(baseUrl);


class OnlineServerService22 extends ReadonlyCommonService<IOnlineServer>{
    constructor(readonly baseUrl2: string = '') {
        super(baseUrl2);
    }
    public sendCommand(serverId: number, command: string) {
        return axios.post(this.baseUrl + serverId + '/command', { serverId, command })
            .then(response => response.data)
            .catch(error => Promise.reject(this.getError(error)));
    }
}

export const onlineServerService2: OnlineServerService22
    = new OnlineServerService22(baseUrl);


