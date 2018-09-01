import {
    combineConstants, commonConstants
} from "../constants";


export function itemReducer(state = {}, action:any, subject:any) {
    switch (action.type) {

        case combineConstants(subject,
            commonConstants.CREATE_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                createRequest: { ...action.item
                }
            };

        case combineConstants(subject,
            commonConstants.CREATE_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                createItem: { ...action.item
                }
            };
        case combineConstants(subject,
            commonConstants.CREATE_ITEM,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        case combineConstants(subject,
            commonConstants.GET_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                request: { ...action.item
                }
            };
        case combineConstants(subject,
            commonConstants.GET_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                item: { ...action.item
                }
            };
        case combineConstants(subject,
            commonConstants.GET_ITEM,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                error: action.error
            };

        case combineConstants(subject,
            commonConstants.UPDATE_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                updateRequest: { ...action.item
                }
            };
        case combineConstants(subject,
            commonConstants.UPDATE_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                updateUser: { ...action.item
                }
            };
        case combineConstants(subject,
            commonConstants.UPDATE_ITEM,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        case combineConstants(subject,
            commonConstants.DELETE_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                deleteRequest: { ...action.item
                }
            };
        case combineConstants(subject,
            commonConstants.DELETE_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state
            };
        case combineConstants(subject,
            commonConstants.DELETE_ITEM,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        default:
            return state
    }
}