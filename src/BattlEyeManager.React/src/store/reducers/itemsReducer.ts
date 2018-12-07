import { ActionConstants, combineConstants, ResultConstants, SubjectConstants } from "../constants";
import { IItemsState, IRequestAction } from "../models";
import { itemReducer } from "./itemReducer";

export function itemsReducer<T>(
    state: IItemsState<T> = {}, action: IRequestAction<T>, subject: SubjectConstants): IItemsState<T> {

    state = itemReducer<T>(state, action, subject);

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
                items: action.items,
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