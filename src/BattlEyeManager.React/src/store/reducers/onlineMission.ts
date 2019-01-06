import { SubjectConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function onlineMissionsReducer(
    state = {}, action: any) {

    const subject = SubjectConstants.ONLINE_MISSION;
    const nestedState = state[action.serverId];

    return {
        ...state,
        [action.serverId]: itemsReducer(nestedState, action, subject)
    }
}