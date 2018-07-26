import { onlineServerConstants } from '../constants';
import {onlineServerService} from '../../services';
import { commonActions } from './commonActions';

export const onlineServerActions = {
    getItems,
    getItem
};

function getItems() {
    return commonActions.getItems(onlineServerConstants.SUBJECT, onlineServerService); 
}

function getItem(id) {
    return commonActions.getItem(id, onlineServerConstants.SUBJECT, onlineServerService); 
}