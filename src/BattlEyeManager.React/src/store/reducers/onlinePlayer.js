import {
    onlinePlayerConstants,
    commonConstants
} from "../constants";

export function onlinePlayersReducer(
    state = {}, action) {

    const subject = onlinePlayerConstants.SUBJECT;

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