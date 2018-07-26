import {
    onlineChatConstants,
    commonConstants
} from "../constants";

export function onlineChatReducer(
    state = {}, action) {

    const subject = onlineChatConstants.SUBJECT;

    switch (action.type) {
        case commonConstants.combine(subject,
            commonConstants.GET_ITEMS,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                [action.serverId]: undefined
            };
        case commonConstants.combine(subject,
            commonConstants.GET_ITEMS,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                ...state,
                [action.serverId]: {
                    items: action.items
                }
            };
        case commonConstants.combine(subject,
            commonConstants.GET_ITEMS,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                ...state,
                [action.serverId]: {
                    items: action.error
                }
            };
        default:
            return state
    }
}