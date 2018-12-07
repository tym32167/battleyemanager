import { SubjectConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function onlineBansReducer(
    state = {}, action: any) {

    const subject = SubjectConstants.ONLINE_BAN;
    const nestedState = state[action.serverId];

    return {
        ...state,
        [action.serverId]: itemsReducer(nestedState, action, subject)
    }
}