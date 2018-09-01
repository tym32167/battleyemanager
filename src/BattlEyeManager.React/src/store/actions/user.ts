import { IUser } from 'src/models';
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

function deleteItem(item: IUser) {
    return commonActions.deleteItem<IUser>(item, userConstants.SUBJECT, userService, (_: any, dispatch: any) => {
        dispatch(getItems());
    });
}

function addItem(item: IUser) {
    return commonActions.addItem<IUser>(item, userConstants.SUBJECT, userService, () => history.push('/users'));
}

function updateItem(item: IUser) {
    return commonActions.updateItem<IUser>(item, userConstants.SUBJECT, userService, () => history.push('/users'));
}

function getItems() {
    return commonActions.getItems<IUser>(userConstants.SUBJECT, userService);
}

function getItem(id: string) {
    return commonActions.getItem<IUser>(id, userConstants.SUBJECT, userService);
}