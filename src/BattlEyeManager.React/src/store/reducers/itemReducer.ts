import { ActionConstants, combineConstants, ResultConstants, SubjectConstants } from "../constants";

export function itemReducer(state = {}, action: any, subject: SubjectConstants) {
    switch (action.type) {

        case combineConstants(subject,
            ActionConstants.CREATE_ITEM,
            ResultConstants.ASYNC_REQUEST):
            return {
                ...state,
                createRequest: {
                    ...action.item
                }
            };

        case combineConstants(subject,
            ActionConstants.CREATE_ITEM,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                createItem: {
                    ...action.item
                }
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
                request: {
                    ...action.item
                }
            };
        case combineConstants(subject,
            ActionConstants.GET_ITEM,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                item: {
                    ...action.item
                }
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
                updateRequest: {
                    ...action.item
                }
            };
        case combineConstants(subject,
            ActionConstants.UPDATE_ITEM,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                updateUser: {
                    ...action.item
                }
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
                deleteRequest: {
                    ...action.item
                }
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