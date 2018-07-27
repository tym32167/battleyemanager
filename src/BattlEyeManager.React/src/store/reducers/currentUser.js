import {  itemReducer } from "./itemReducer";
import { currentUserConstants } from "../constants";

export function currentUsersReducer(state = {item : {}}, action) {
    return itemReducer(state, action, currentUserConstants.SUBJECT);
}
