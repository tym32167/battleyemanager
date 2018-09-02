import { SubjectConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function onlineChatReducer(
    state = {}, action: any) {

    const subject = SubjectConstants.ONLINE_CHAT;
    const nestedState = state[action.serverId];

    return {
        ...state,
        [action.serverId]: itemsReducer(nestedState, action, subject)
    }
}