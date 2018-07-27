import {
    commonActions
} from "./commonActions";
import {
    currentUserConstants
} from "../constants";
import {
    currentUserService
} from '../../services';

export const currentUserActions = {   
    getItem
};

function getItem() {
    return commonActions.getItem('', currentUserConstants.SUBJECT, currentUserService);
}