import { ActionConstants, combineConstants, ResultConstants } from "../constants";
import { itemReducer } from "./itemReducer";

export function itemsReducer(
    state: any = {
        items: [],
    }, action: any, subject: any) {

    state = itemReducer(state, action, subject);

    switch (action.type) {
        case combineConstants(subject,
            ActionConstants.GET_ITEMS,
            ResultConstants.ASYNC_REQUEST):
            return {
                busy: true,
                ...state,
                error: '',

            };
        case combineConstants(subject,
            ActionConstants.GET_ITEMS,
            ResultConstants.ASYNC_REQUEST_SUCCESS):
            return {
                busy: false,
                items: [...action.items],
            };
        case combineConstants(subject,
            ActionConstants.GET_ITEMS,
            ResultConstants.ASYNC_REQUEST_FAILURE):
            return {
                busy: false,
                error: action.error,
            };
        default:
            return state
    }
}