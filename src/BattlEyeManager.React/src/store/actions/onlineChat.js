import { onlineChatConstants, commonConstants } from '../constants';
import {onlineChatService} from '../../services';

export const onlineChatActions = {
    getItems,
    addItem
};

function getItems(serverId) {
    return dispatch => {
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
    function request(serverId, items) { return { serverId:serverId, 
        type: commonConstants.combine(onlineChatConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST), 
        items } }
    function success(serverId, items) { return { serverId:serverId, 
        type: commonConstants.combine(onlineChatConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_SUCCESS), 
        items } }
    function failure(serverId, error) { return { serverId:serverId, 
        type: commonConstants.combine(onlineChatConstants.SUBJECT, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_FAILURE), 
        error } }
}



function addItem(serverId, item) {
    return dispatch => {                
        onlineChatService.addItem(serverId, item)
            .then(
                item => {                    
                },
                error => {                    
                }
            );
    };    
}

