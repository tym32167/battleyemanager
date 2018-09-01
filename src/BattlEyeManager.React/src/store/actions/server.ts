import { history, serverService } from '../../services';
import { serverConstants } from '../constants';
import { commonActions } from './commonActions';
import { onlineServerActions } from './onlineServer';

export const serverActions = {
    addItem,
    deleteItem,
    getItem,
    getItems,
    updateItem,
};

function getItems() {
    return commonActions.getItems(serverConstants.SUBJECT, serverService);
}

function addItem(item: any) {
    return commonActions.addItem(item, serverConstants.SUBJECT, serverService, (_: any, dispatch: any) => {
        dispatch(onlineServerActions.getItems());
        history.push('/servers');
    });
}

function deleteItem(item: any) {
    return commonActions.deleteItem(item, serverConstants.SUBJECT, serverService, (_: any, dispatch: any) => {
        dispatch(getItems());
        dispatch(onlineServerActions.getItems());
    });
}

function updateItem(item: any) {
    return commonActions.updateItem(item, serverConstants.SUBJECT, serverService, (_: any, dispatch: any) => {
        dispatch(onlineServerActions.getItems());
        history.push('/servers');
    });
}

function getItem(id: any) {
    return commonActions.getItem(id, serverConstants.SUBJECT, serverService);
}