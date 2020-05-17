import axios from 'axios';
import { IOnlinePlayer } from 'src/models';
import { ReadonlyCommonService } from './core/readonlycommonservice';

class OnlinePlayersService {
    constructor(readonly baseUrl: string = '', readonly service: ReadonlyCommonService<IOnlinePlayer>) {
    }

    public getItems(serverId: string | number) {
        return this.service.getItemsBy(this.baseUrl + serverId + '/players/');
    }

    public kickPlayer(serverId: string | number, reason: string, player: IOnlinePlayer) {
        return axios.post(this.baseUrl + serverId + '/kick', {
            player,
            reason,
            serverId
        })
            .catch(error => Promise.reject(this.service.getError(error)));
    }


    public BanPlayerOnline(serverId: string | number, minutes: number, reason: string, player: IOnlinePlayer) {
        return axios.post(this.baseUrl + serverId + '/ban', {
            minutes,
            player,
            reason,
            serverId
        })
            .catch(error => Promise.reject(this.service.getError(error)));
    }
}


export const onlinePlayersService = new OnlinePlayersService('/api/onlineserver/', new ReadonlyCommonService<IOnlinePlayer>());
