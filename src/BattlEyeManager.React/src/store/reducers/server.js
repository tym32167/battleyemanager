import { serverConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function serversReducer(
    state, action) {

    return itemsReducer(state, action, serverConstants.SUBJECT);
}