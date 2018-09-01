import { userConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function usersReducer(state: any, action: any) {
    return itemsReducer(state, action, userConstants.SUBJECT);
}
