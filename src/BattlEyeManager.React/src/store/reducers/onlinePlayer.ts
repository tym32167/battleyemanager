import { SubjectConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function onlinePlayersReducer(
    state = {}, action: any) {

    const subject = SubjectConstants.ONLINE_PLAYER;
    const nestedState = state[action.serverId];

    return {
        ...state,
        [action.serverId]: itemsReducer(nestedState, action, subject)
    }
}