import { IOnlinePlayer } from 'src/models';
import { onlinePlayersService } from '../../services';
import { ActionConstants, combineConstants, ResultConstants, SubjectConstants } from '../constants';

export const onlinePlayerActions = {
    banPlayerOnline,
    getItems,
    kickPlayer,
};

function getItems(serverId: any) {
    return (dispatch: any) => {
        dispatch(request(serverId, []));
        onlinePlayersService.getItems(serverId)
            .then(
                items => {
                    dispatch(success(serverId, items));
                },
                error => {
                    dispatch(failure(serverId, error));
                }
            );
    };
}

function kickPlayer(serverId: number | string, player: IOnlinePlayer, reason: string) {

    return (dispatch: any) => {
        dispatch(requestKick(serverId, player, reason));
        onlinePlayersService.kickPlayer(serverId, reason, player)
            .then(
                _ => {
                    dispatch(successKick(serverId));
                },
                error => {
                    dispatch(failureKick(serverId, error));
                }
            );
    };
}

function banPlayerOnline(serverId: number | string, minutes: number, player: IOnlinePlayer, reason: string) {
    return (dispatch: any) => {
        dispatch(requestBan(serverId, player, reason, minutes));
        onlinePlayersService.BanPlayerOnline(serverId, minutes, reason, player)
            .then(
                _ => {
                    dispatch(successBan(serverId));
                },
                error => {
                    dispatch(failureBan(serverId, error));
                }
            );
    };
}



function requestBan(serverId: any, player: IOnlinePlayer, reason: string, minutes: number) {
    return {
        kick: {
            minutes,
            player,
            reason,
            serverId
        },
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.BAN_PLAYER, ResultConstants.ASYNC_REQUEST),
    }
}
function successBan(serverId: any) {
    return {
        kick: {},
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.BAN_PLAYER, ResultConstants.ASYNC_REQUEST_SUCCESS),
    }
}
function failureBan(serverId: any, error: any) {
    return {
        error,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.BAN_PLAYER, ResultConstants.ASYNC_REQUEST_FAILURE),
    }
}


function requestKick(serverId: any, player: IOnlinePlayer, reason: string) {
    return {
        kick: {
            player,
            reason,
            serverId
        },
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.KICK_PLAYER, ResultConstants.ASYNC_REQUEST),
    }
}
function successKick(serverId: any) {
    return {
        kick: {},
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.KICK_PLAYER, ResultConstants.ASYNC_REQUEST_SUCCESS),
    }
}
function failureKick(serverId: any, error: any) {
    return {
        error,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.KICK_PLAYER, ResultConstants.ASYNC_REQUEST_FAILURE),
    }
}



function request(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST),
    }
}
function success(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST_SUCCESS),
    }
}
function failure(serverId: any, error: any) {
    return {
        error,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_PLAYER, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST_FAILURE),
    }
}

