import { serverConstants } from '../constants';
import {serverService, history} from '../../services';
import { onlineServerActions } from './onlineServer';
import { commonActions } from './commonActions';

export const serverActions = {
    getItems,
    getItem,
    updateItem,
    addItem,
    deleteItem
};

function getItems() {
    return commonActions.getItems(serverConstants.SUBJECT, serverService); 
}

function addItem(item) {
    return commonActions.addItem(item, serverConstants.SUBJECT, serverService, (item, dispatch)=>{
        dispatch(onlineServerActions.getItems());
        history.push('/servers');
    });    
}

function deleteItem(item) {
    return commonActions.deleteItem(item, serverConstants.SUBJECT, serverService, (item, dispatch)=>{
        dispatch(getItems());
        dispatch(onlineServerActions.getItems());
    });
}

function updateItem(item) {
    return commonActions.updateItem(item, serverConstants.SUBJECT, serverService, (item, dispatch)=>{
        dispatch(onlineServerActions.getItems());
        history.push('/servers');
    }); 
}

function getItem(id) {
    return commonActions.getItem(id, serverConstants.SUBJECT, serverService); 
}