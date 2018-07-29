import { banReasonConstants } from '../constants';
import {banReasonsService} from '../../services';
import { commonActions } from './commonActions';

export const banReasonActions = {
    getItems
};

function getItems() {
    return commonActions.getItems(banReasonConstants.SUBJECT, banReasonsService); 
}