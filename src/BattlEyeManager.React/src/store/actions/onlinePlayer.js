import { onlinePlayerConstants, commonConstants } from '../constants';
import {onlinePlayersService} from '../../services';

export const onlinePlayerActions = {
    getItems
};

function getItems(serverId) {
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
    function request(serverId, items) { return { serverId:serverId, 
        type: commonConstants.combine(onlinePlayerConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST), 
        items } }
    function success(serverId, items) { return { serverId:serverId, 
        type: commonConstants.combine(onlinePlayerConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_SUCCESS), 
        items } }
    function failure(serverId, error) { return { serverId:serverId, 
        type: commonConstants.combine(onlinePlayerConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_FAILURE), 
        error } }
}

