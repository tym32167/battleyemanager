import { kickReasonConstants } from '../constants';
import {kickReasonsService} from '../../services';
import { commonActions } from './commonActions';

export const kickReasonActions = {
    getItems
};

function getItems() {
    return commonActions.getItems(kickReasonConstants.SUBJECT, kickReasonsService); 
}