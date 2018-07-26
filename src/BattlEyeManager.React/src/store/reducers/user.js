import { itemsReducer } from "./itemsReducer";
import { userConstants } from "../constants";

export function usersReducer(state, action){
   return itemsReducer(state, action, userConstants.SUBJECT);
}
