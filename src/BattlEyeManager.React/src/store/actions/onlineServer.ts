import { onlineServerService } from '../../services';
import { onlineServerConstants } from '../constants';
import { commonActions } from './commonActions';

export const onlineServerActions = {
    getItem,
    getItems,
};

function getItems() {
    return commonActions.getItems(onlineServerConstants.SUBJECT, onlineServerService);
}

function getItem(id: any) {
    return commonActions.getItem(id, onlineServerConstants.SUBJECT, onlineServerService);
}