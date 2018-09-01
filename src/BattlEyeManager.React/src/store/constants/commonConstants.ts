export enum commonConstants {
    GET_ITEM= 'GET_ITEM',
    GET_ITEMS= 'GET_ITEMS',
    UPDATE_ITEM= 'UPDATE_ITEM',
    CREATE_ITEM= 'CREATE_ITEM',
    DELETE_ITEM='DELETE_ITEM',

    ASYNC_REQUEST= 'ASYNC_REQUEST',
    ASYNC_REQUEST_SUCCESS= 'ASYNC_REQUEST_SUCCESS',
    ASYNC_REQUEST_FAILURE= 'ASYNC_REQUEST_FAILURE',    
};

export const combineConstants = (subject:any, action:any, result:any) => {
    return subject + '_' + action + '_' + result;
}