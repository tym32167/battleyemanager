import { onlinePlayerConstants } from '../constants';
import {onlinePlayersService} from '../../services';

export const onlinePlayerActions = {
    getAll
};

function getAll(serverId) {
    return dispatch => {
        dispatch(request(serverId, []));
        onlinePlayersService.getAll(serverId)
            .then(
                items => {
                    dispatch(success(serverId, items));
                },
                error => {
                    dispatch(failure(serverId, error));
                }
            );
    };
    function request(serverId, items) { return { serverId:serverId, type: onlinePlayerConstants.GET_ONLINE_PLAYERS_REQUEST, items } }
    function success(serverId, items) { return { serverId:serverId, type: onlinePlayerConstants.GET_ONLINE_PLAYERS_SUCCESS, items } }
    function failure(serverId, error) { return { serverId:serverId, type: onlinePlayerConstants.GET_ONLINE_PLAYERS_FAILURE, error } }
}

