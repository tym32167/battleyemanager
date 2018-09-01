import { IIdentity } from "src/models/iidentity";
import { IReadonlyService, IService } from "../../services";
import { combineConstants, commonConstants } from "../constants";

export const commonActions = {
    addItem,
    deleteItem,
    getItem,
    getItems,
    updateItem,
};

function deleteItem<T extends IIdentity>(element: T, subject: string, service: IService<T>, successAction: any) {
    return (dispatch: any) => {
        dispatch(request(element));
        service.deleteItem(element.id)
            .then(
                (item: any) => {
                    dispatch(success(item));
                    if (successAction) { successAction(item, dispatch); }
                },
                (error: any) => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item: any) { return { type: combineConstants(subject, commonConstants.DELETE_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item: any) { return { type: combineConstants(subject, commonConstants.DELETE_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error: any) { return { type: combineConstants(subject, commonConstants.DELETE_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function addItem<T extends IIdentity>(element: T, subject: string, service: IService<T>, successAction: any) {
    return (dispatch: any) => {
        dispatch(request(element));
        service.addItem(element)
            .then(
                (item: T) => {
                    dispatch(success(item));
                    if (successAction) { successAction(item, dispatch); }
                },
                (error: any) => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item: any) { return { type: combineConstants(subject, commonConstants.CREATE_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item: any) { return { type: combineConstants(subject, commonConstants.CREATE_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error: any) { return { type: combineConstants(subject, commonConstants.CREATE_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function updateItem<T extends IIdentity>(element: T, subject: string, service: IService<T>, successAction: any) {
    return (dispatch: any) => {
        dispatch(request(element));
        service.updateItem(element)
            .then(
                (item: any) => {
                    dispatch(success(item));
                    if (successAction) { successAction(item, dispatch); }
                },
                (error: any) => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item: any) { return { type: combineConstants(subject, commonConstants.UPDATE_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item: any) { return { type: combineConstants(subject, commonConstants.UPDATE_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error: any) { return { type: combineConstants(subject, commonConstants.UPDATE_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function getItems<T>(subject: string, service: IReadonlyService<T>) {
    return (dispatch: any) => {
        dispatch(request([]));
        getPromise(service, (s: any) => s.getItems())
            .then(
                (items: any) => {
                    dispatch(success(items));
                },
                (error: any) => {
                    dispatch(failure(error));
                }
            );
    };
    function request(items: any) { return { type: combineConstants(subject, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST), items } }
    function success(items: any) { return { type: combineConstants(subject, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_SUCCESS), items } }
    function failure(error: any) { return { type: combineConstants(subject, commonConstants.GET_ITEMS, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function getItem<T>(id: string | number, subject: string, service: IReadonlyService<T>) {
    return (dispatch: any) => {
        dispatch(request({ id }));
        service.getItem(id)
            .then(
                (user: any) => {
                    dispatch(success(user));
                },
                (error: any) => {
                    dispatch(failure(error));
                }
            );
    };
    function request(item: any) { return { type: combineConstants(subject, commonConstants.GET_ITEM, commonConstants.ASYNC_REQUEST), item } }
    function success(item: any) { return { type: combineConstants(subject, commonConstants.GET_ITEM, commonConstants.ASYNC_REQUEST_SUCCESS), item } }
    function failure(error: any) { return { type: combineConstants(subject, commonConstants.GET_ITEM, commonConstants.ASYNC_REQUEST_FAILURE), error } }
}

function getPromise(subject: any, invocator: any) {
    if (typeof (subject) === 'function') { return subject(); }
    return invocator(subject);
}
