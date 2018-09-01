import { kickReasonConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function kickReasonsReducer(
    state: any, action: any) {
    return itemsReducer(state, action, kickReasonConstants.SUBJECT);
}