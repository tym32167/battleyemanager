import {
    itemReducer
} from "./itemReducer";
import {
    commonConstants
} from "../constants";

export function itemsReducer(
    state = {
        items: [],
    }, action, subject) {

    state = itemReducer(state, action, subject);

    switch (action.type) {
        case commonConstants.combine(subject,
            commonConstants.GET_ITEMS,
            commonConstants.ASYNC_REQUEST):
            return {
                ...state,
                error: '',
                busy : true
            };
        case commonConstants.combine(subject,
            commonConstants.GET_ITEMS,
            commonConstants.ASYNC_REQUEST_SUCCESS):
            return {
                items: [...action.items],
                busy : false
            };
        case commonConstants.combine(subject,
            commonConstants.GET_ITEMS,
            commonConstants.ASYNC_REQUEST_FAILURE):
            return {
                error: action.error,
                busy : false
            };
        default:
            return state
    }
}