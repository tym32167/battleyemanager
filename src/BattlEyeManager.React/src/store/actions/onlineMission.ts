import { onlineMissionsService } from '../../services';
import { ActionConstants, combineConstants, ResultConstants, SubjectConstants } from '../constants';

export const onlineMissionActions = {
    getItems
};

function getItems(serverId: any) {
    return (dispatch: any) => {
        dispatch(request(serverId, []));
        onlineMissionsService.getItems(serverId)
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
        type: combineConstants(SubjectConstants.ONLINE_MISSION, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST),
    }
}
function success(serverId: any, items: any) {
    return {
        items,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_MISSION, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST_SUCCESS),
    }
}
function failure(serverId: any, error: any) {
    return {
        error,
        serverId,
        type: combineConstants(SubjectConstants.ONLINE_MISSION, ActionConstants.GET_ITEMS, ResultConstants.ASYNC_REQUEST_FAILURE),
    }
}

