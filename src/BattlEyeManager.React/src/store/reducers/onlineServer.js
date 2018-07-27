import { onlineServerConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function onlineServersReducer(state, action) {
    return itemsReducer(state, action, onlineServerConstants.SUBJECT);
}
