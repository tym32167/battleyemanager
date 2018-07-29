import { banReasonConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function banReasonsReducer(
    state, action) {
    return itemsReducer(state, action, banReasonConstants.SUBJECT);
}