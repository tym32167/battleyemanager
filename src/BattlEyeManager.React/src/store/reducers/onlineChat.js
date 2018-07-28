import {     onlineChatConstants } from "../constants";
import {     itemsReducer } from "./itemsReducer";

export function onlineChatReducer(
    state = {}, action) {

    const subject = onlineChatConstants.SUBJECT;
    const nestedState = state[action.serverId];

    return {
        ...state,
        [action.serverId] : itemsReducer(nestedState, action, subject)
    }
}