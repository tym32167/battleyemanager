import { ActionConstants, combineConstants, ResultConstants, SubjectConstants } from "../constants";
import { IItemsState, IRequestAction } from "../models";

export function itemReducer<T>(state: IItemsState<T> = {}, action: IRequestAction<T>, subject: SubjectConstants): IItemsState<T> {
    switch (action.type) {

        case combineConstants(subject,
            ActionConstants.CREATE_ITEM,
            ResultConstants.ASYNC_REQUEST):
            return {
                ...state,
                createItemRequest: action.item
            };

        case combineConstants(subject,
            ActionConstants.CREATE_ITEM,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                createItem: action.item
            };
        case combineConstants(subject,
            ActionConstants.CREATE_ITEM,
            ResultConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        case combineConstants(subject,
            ActionConstants.GET_ITEM,
            ResultConstants.ASYNC_REQUEST):
            return {
                ...state,
                request: action.item
            };
        case combineConstants(subject,
            ActionConstants.GET_ITEM,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                item: action.item
            };
        case combineConstants(subject,
            ActionConstants.GET_ITEM,
            ResultConstants.ASYNC_REQUEST_FAILURE):
            return {
                error: action.error
            };

        case combineConstants(subject,
            ActionConstants.UPDATE_ITEM,
            ResultConstants.ASYNC_REQUEST):
            return {
                ...state,
                updateItemRequest: action.item
            };
        case combineConstants(subject,
            ActionConstants.UPDATE_ITEM,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                updateItem: action.item
            };
        case combineConstants(subject,
            ActionConstants.UPDATE_ITEM,
            ResultConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        case combineConstants(subject,
            ActionConstants.DELETE_ITEM,
            ResultConstants.ASYNC_REQUEST):
            return {
                ...state,
                deleteItemRequest: action.item
            };
        case combineConstants(subject,
            ActionConstants.DELETE_ITEM,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state
            };
        case combineConstants(subject,
            ActionConstants.DELETE_ITEM,
            ResultConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        default:
            return state
    }
}