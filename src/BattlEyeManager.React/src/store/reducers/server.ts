import { serverConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function serversReducer(
    state: any, action: any) {

    return itemsReducer(state, action, serverConstants.SUBJECT);
}