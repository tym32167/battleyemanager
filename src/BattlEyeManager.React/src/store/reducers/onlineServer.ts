import { onlineServerConstants } from "../constants";
import { itemsReducer } from "./itemsReducer";

export function onlineServersReducer(state: any, action: any) {
    return itemsReducer(state, action, onlineServerConstants.SUBJECT);
}
