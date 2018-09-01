import { Dispatch } from "redux";
import { IIdentity } from "src/models/iidentity";
import { IReadonlyService, IService } from "../../services";
import { combineConstants, commonConstants } from "../constants";


function requestAction<T>(item: T, subject: string, action: commonConstants) {
    return { type: combineConstants(subject, action, commonConstants.ASYNC_REQUEST), item };
}

function successAction<T>(item: T, subject: string, action: commonConstants) {
    return { type: combineConstants(subject, action, commonConstants.ASYNC_REQUEST_SUCCESS), item };
}

function failureAction(error: any, subject: string, action: commonConstants) {
    return { type: combineConstants(subject, action, commonConstants.ASYNC_REQUEST_FAILURE), error };
}


class CommonActions {
    public deleteItem<T extends IIdentity>(element: T, subject: string, service: IService<T>, callback: any) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction(element, subject, commonConstants.DELETE_ITEM));
            service.deleteItem(element.id)
                .then(
                    (item: T) => {
                        dispatch(callback(item, subject, commonConstants.DELETE_ITEM));
                        if (callback) { callback(item, dispatch); }
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, commonConstants.DELETE_ITEM));
                    }
                );
        };
    }

    public addItem<T extends IIdentity>(element: T, subject: string, service: IService<T>, callback: any) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction(element, subject, commonConstants.CREATE_ITEM));
            service.addItem(element)
                .then(
                    (item: T) => {
                        dispatch(callback(item, subject, commonConstants.CREATE_ITEM));
                        if (callback) { callback(item, dispatch); }
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, commonConstants.CREATE_ITEM));
                    }
                );
        };
    }

    public updateItem<T extends IIdentity>(element: T, subject: string, service: IService<T>, callback: any) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction(element, subject, commonConstants.UPDATE_ITEM));
            service.updateItem(element)
                .then(
                    (item: T) => {
                        dispatch(callback(item, subject, commonConstants.UPDATE_ITEM));
                        if (callback) { callback(item, dispatch); }
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, commonConstants.UPDATE_ITEM));
                    }
                );
        };
    }

    public getItems<T>(subject: string, service: IReadonlyService<T>) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction([], subject, commonConstants.GET_ITEMS));
            this.getPromise(service, (s: any) => s.getItems())
                .then(
                    (items: T[]) => {
                        dispatch(successAction(items, subject, commonConstants.GET_ITEMS));
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, commonConstants.GET_ITEMS));
                    }
                );
        };
    }

    public getItem<T>(id: string | number, subject: string, service: IReadonlyService<T>) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction({ id }, subject, commonConstants.GET_ITEM));
            service.getItem(id)
                .then(
                    (item: T) => {
                        dispatch(successAction(item, subject, commonConstants.GET_ITEM));
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, commonConstants.GET_ITEM));
                    }
                );
        };
    }

    private getPromise(subject: any, invocator: any) {
        if (typeof (subject) === 'function') { return subject(); }
        return invocator(subject);
    }

}

export const commonActions = new CommonActions();