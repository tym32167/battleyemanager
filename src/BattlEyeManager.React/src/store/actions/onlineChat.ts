import { onlineChatService } from '../../services';
import { combineConstants, commonConstants, onlineChatConstants } from '../constants';

export const onlineChatActions = {
    addItem,
    getItems,
};

function getItems(serverId: any) {
    return (dispatch: any) => {
        dispatch(request(serverId, []));
        onlineChatService.getItems(serverId)
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
        type: combineConstants(onlineChatConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST),
    }
}
function success(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(onlineChatConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_SUCCESS),
    }
}
function failure(serverId: any, error: any) {
    return {
        error,
        serverId,
        type: combineConstants(onlineChatConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_FAILURE)
    }
}


function addItem(serverId: any, item: any) {
    return (dispatch: any) => {
        onlineChatService.addItem(serverId, item);
    };
}

