import { Dispatch } from "redux";
import { IIdentity } from "src/models/iidentity";
import { IReadonlyService, IService } from "../../services";
import { ActionConstants, combineConstants, ResultConstants, SubjectConstants } from "../constants";


function requestAction<T>(item: T, subject: SubjectConstants, action: ActionConstants) {
    return { type: combineConstants(subject, action, ResultConstants.ASYNC_REQUEST), item };
}

function successAction<T>(item: T, subject: SubjectConstants, action: ActionConstants) {
    return { type: combineConstants(subject, action, ResultConstants.ASYNC_REQUEST_SUCCESS), item };
}

function successActionMany<T>(items: T[], subject: SubjectConstants, action: ActionConstants) {
    return { type: combineConstants(subject, action, ResultConstants.ASYNC_REQUEST_SUCCESS), items };
}

function failureAction(error: any, subject: SubjectConstants, action: ActionConstants) {
    return { type: combineConstants(subject, action, ResultConstants.ASYNC_REQUEST_FAILURE), error };
}

export class CommonActions {
    public deleteItem<T extends IIdentity>(element: T, subject: SubjectConstants, service: IService<T>, callback: any) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction(element, subject, ActionConstants.DELETE_ITEM));
            service.deleteItem(element.id)
                .then(
                    (item: T) => {
                        dispatch(successAction(item, subject, ActionConstants.DELETE_ITEM));
                        if (callback) { callback(item, dispatch); }
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, ActionConstants.DELETE_ITEM));
                    }
                );
        };
    }

    public addItem<T extends IIdentity>(element: T, subject: SubjectConstants, service: IService<T>, callback: any) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction(element, subject, ActionConstants.CREATE_ITEM));
            service.addItem(element)
                .then(
                    (item: T) => {
                        dispatch(successAction(item, subject, ActionConstants.CREATE_ITEM));
                        if (callback) { callback(item, dispatch); }
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, ActionConstants.CREATE_ITEM));
                    }
                );
        };
    }

    public updateItem<T extends IIdentity>(element: T, subject: SubjectConstants, service: IService<T>, callback: any) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction(element, subject, ActionConstants.UPDATE_ITEM));
            service.updateItem(element)
                .then(
                    (item: T) => {
                        dispatch(successAction(item, subject, ActionConstants.UPDATE_ITEM));
                        if (callback) { callback(item, dispatch); }
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, ActionConstants.UPDATE_ITEM));
                    }
                );
        };
    }

    public getItems<T>(subject: SubjectConstants, service: IReadonlyService<T>) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction([], subject, ActionConstants.GET_ITEMS));
            this.getPromise(service, (s: any) => s.getItems())
                .then(
                    (items: T[]) => {
                        dispatch(successActionMany(items, subject, ActionConstants.GET_ITEMS));
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, ActionConstants.GET_ITEMS));
                    }
                );
        };
    }

    public getItem<T>(id: string | number, subject: SubjectConstants, service: IReadonlyService<T>) {
        return (dispatch: Dispatch<T>) => {
            dispatch(requestAction({ id }, subject, ActionConstants.GET_ITEM));
            service.getItem(id)
                .then(
                    (item: T) => {
                        dispatch(successAction(item, subject, ActionConstants.GET_ITEM));
                    },
                    (error: any) => {
                        dispatch(failureAction(error, subject, ActionConstants.GET_ITEM));
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