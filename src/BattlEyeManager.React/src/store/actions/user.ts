import { history, userService } from '../../services';
import { userConstants } from "../constants";
import { commonActions } from "./commonActions";

export const userActions = {
    addItem,
    deleteItem,
    getItem,
    getItems,
    updateItem,
};

function deleteItem(item: any) {
    return commonActions.deleteItem(item, userConstants.SUBJECT, userService, (_: any, dispatch: any) => {
        dispatch(getItems());
    });
}

function addItem(item: any) {
    return commonActions.addItem(item, userConstants.SUBJECT, userService, () => history.push('/users'));
}

function updateItem(item: any) {
    return commonActions.updateItem(item, userConstants.SUBJECT, userService, () => history.push('/users'));
}

function getItems() {
    return commonActions.getItems(userConstants.SUBJECT, userService);
}

function getItem(id: any) {
    return commonActions.getItem(id, userConstants.SUBJECT, userService);
}