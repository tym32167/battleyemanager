import { currentUserConstants } from "../constants";
import { itemReducer } from "./itemReducer";

export function currentUsersReducer(state = { item: {} }, action: any) {
    return itemReducer(state, action, currentUserConstants.SUBJECT);
}
