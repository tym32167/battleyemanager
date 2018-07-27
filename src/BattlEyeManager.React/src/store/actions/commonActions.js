import { commonConstants } from "../constants";

export const commonActions = {
    getItems,
    getItem,
    updateItem,
    addItem,
    deleteItem
};

function deleteItem(item, subject, service, successAction) {
    return dispatch => {
        dispatch(request(item));
        service.deleteItem(item.id)
            .then(
                item => {
                    dispatch(success(item));
                    if (successAction) successAction(item, dispatch);
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item)  { return { type: commonConstants.combine(subject, commonConstants.DELETE_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item)  { return { type: commonConstants.combine(subject, commonConstants.DELETE_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error) { return { type: commonConstants.combine(subject, commonConstants.DELETE_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function addItem(item, subject, service, successAction) {
    return dispatch => {
        dispatch(request(item));
        service.addItem(item)
            .then(
                user => {
                    dispatch(success(item));
                    if (successAction) successAction(item, dispatch);
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item)  { return { type: commonConstants.combine(subject, commonConstants.CREATE_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item)  { return { type: commonConstants.combine(subject, commonConstants.CREATE_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error) { return { type: commonConstants.combine(subject, commonConstants.CREATE_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function updateItem(item, subject, service, successAction) {
    return dispatch => {
        dispatch(request(item));
        service.updateItem(item)
            .then(
                item => {
                    dispatch(success(item));
                    if (successAction) successAction(item, dispatch);
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item)  { return { type: commonConstants.combine(subject, commonConstants.UPDATE_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item)  { return { type: commonConstants.combine(subject, commonConstants.UPDATE_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error) { return { type: commonConstants.combine(subject, commonConstants.UPDATE_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function getItems(subject, service) {
    return dispatch => {
        dispatch(request([]));
        getPromise(service, s=>s.getItems())
            .then(
                items => {
                    dispatch(success(items));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(items)  { return { type: commonConstants.combine(subject, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST), items } }
    function success(items)  { return { type: commonConstants.combine(subject, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_SUCCESS), items } }
    function failure(error) { return { type: commonConstants.combine(subject, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function getItem(id, subject, service) {
    return dispatch => {
        dispatch(request({id}));
        service.getItem(id)
            .then(
                user => {
                    dispatch(success(user));
                },
                error => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item)  { return { type: commonConstants.combine(subject, commonConstants.GET_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item)  { return { type: commonConstants.combine(subject, commonConstants.GET_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error) { return { type: commonConstants.combine(subject, commonConstants.GET_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function getPromise(subject, invocator){
    if(typeof(subject) === 'function') return subject();
    return invocator(subject);
}
