import { banReasonConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function banReasonsReducer(
    state: any, action: any) {
    return itemsReducer(state, action, banReasonConstants.SUBJECT);
}