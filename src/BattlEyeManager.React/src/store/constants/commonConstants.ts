import { SubjectConstants } from "./subject.contsnts";

export enum ActionConstants {
    GET_ITEM = 'GET_ITEM',
    GET_ITEMS = 'GET_ITEMS',
    UPDATE_ITEM = 'UPDATE_ITEM',
    CREATE_ITEM = 'CREATE_ITEM',
    DELETE_ITEM = 'DELETE_ITEM',
};

export enum ResultConstants {
    ASYNC_REQUEST = 'ASYNC_REQUEST',
    ASYNC_REQUEST_SUCCESS = 'ASYNC_REQUEST_SUCCESS',
    ASYNC_REQUEST_FAILURE = 'ASYNC_REQUEST_FAILURE',
};


export const combineConstants = (subject: SubjectConstants, action: ActionConstants, result: ResultConstants) => {
    return subject + '_' + action + '_' + result;
}