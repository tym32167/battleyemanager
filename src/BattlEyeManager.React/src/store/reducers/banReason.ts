import { SubjectConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function banReasonsReducer(
    state: any, action: any) {
    return itemsReducer(state, action, SubjectConstants.BAN_REASON);
}