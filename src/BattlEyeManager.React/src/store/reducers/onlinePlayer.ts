import { onlinePlayerConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function onlinePlayersReducer(
    state = {}, action: any) {

    const subject = onlinePlayerConstants.SUBJECT;
    const nestedState = state[action.serverId];

    return {
        ...state,
        [action.serverId]: itemsReducer(nestedState, action, subject)
    }
}