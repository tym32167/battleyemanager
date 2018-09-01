import { onlinePlayersService } from '../../services';
import { combineConstants, commonConstants, onlinePlayerConstants } from '../constants';

export const onlinePlayerActions = {
    getItems
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

function request(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(onlinePlayerConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST),
    }
}
function success(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(onlinePlayerConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_SUCCESS),
    }
}
function failure(serverId: any, error: any) {
    return {
        error,
        serverId,
        type: combineConstants(onlinePlayerConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_FAILURE),
    }
}

