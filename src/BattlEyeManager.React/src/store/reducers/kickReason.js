import { kickReasonConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function kickReasonsReducer(
    state, action) {
    return itemsReducer(state, action, kickReasonConstants.SUBJECT);
}