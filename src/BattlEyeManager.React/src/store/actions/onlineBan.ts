import { onlineBansService } from '../../services';
import { ActionConstants, combineConstants, ResultConstants, SubjectConstants } from '../constants';

export const onlineBanActions = {
    getItems,
    removeBan
};

function getItems(serverId: any) {
    return (dispatch: any) => {
        dispatch(request(serverId, []));
        onlineBansService.getItems(serverId)
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

function removeBan(serverId: number, banNumber: number) {
    return (dispatch: any) => {
        dispatch(request(serverId, []));
        onlineBansService.removeBan(serverId, banNumber);
    };
}

function request(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_BAN, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST),
    }
}
function success(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_BAN, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST_SUCCESS),
    }
}
function failure(serverId: any, error: any) {
    return {
        error,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_BAN, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST_FAILURE),
    }
}

