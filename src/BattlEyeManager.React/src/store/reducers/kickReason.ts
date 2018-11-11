import { SubjectConstants } from '../constants';
import { itemsReducer } from './itemsReducer';

export function kickReasonsReducer(state: any, action: any) {
    return itemsReducer(state, action, SubjectConstants.KICK_REASON);
}