import { commonActions } from "./commonActions";
import { userConstants } from "../constants";
import  {userService, history} from '../../services';

export const userActions = {
    getItems,
    getItem,
    updateItem,
    addItem,
    deleteItem
};

function deleteItem(item) {
    return commonActions.deleteItem(item, userConstants.SUBJECT, userService, (item, dispatch)=>{
        dispatch(getItems());
    });
}

function addItem(item) {
    return commonActions.addItem(item, userConstants.SUBJECT, userService, ()=>history.push('/users'));    
}

function updateItem(item) {
    return commonActions.updateItem(item, userConstants.SUBJECT, userService, ()=>history.push('/users')); 
}

function getItems() {
    return commonActions.getItems(userConstants.SUBJECT, userService); 
}

function getItem(id) {
    return commonActions.getItem(id, userConstants.SUBJECT, userService); 
}