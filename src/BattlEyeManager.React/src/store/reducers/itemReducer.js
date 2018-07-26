import {
    commonConstants
} from "../constants";


export function itemReducer(state = {}, action, subject) {
    switch (action.type) {

        case commonConstants.combine(subject,
            commonConstants.CREATE_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                createRequest: { ...action.item  }
            };

        case commonConstants.combine(subject,
            commonConstants.CREATE_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                createItem: { ...action.item  }
            };
        case commonConstants.combine(subject,
            commonConstants.CREATE_ITEM,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        case commonConstants.combine(subject,
            commonConstants.GET_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                request: { ...action.item  }
            };
        case commonConstants.combine(subject,
            commonConstants.GET_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                item: { ...action.item    }
            };
        case commonConstants.combine(subject,
            commonConstants.GET_ITEM,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                error: action.error
            };

        case commonConstants.combine(subject,
            commonConstants.UPDATE_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                updateRequest: { ...action.item    }
            };
        case commonConstants.combine(subject,
            commonConstants.UPDATE_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                updateUser: { ...action.item   }
            };
        case commonConstants.combine(subject,
            commonConstants.UPDATE_ITEM,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                error: action.error
            };

        case commonConstants.combine(subject,
            commonConstants.DELETE_ITEM,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                deleteRequest: { ...action.item  }
            };
        case commonConstants.combine(subject,
            commonConstants.DELETE_ITEM,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state
            };
        case commonConstants.combine(subject,
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