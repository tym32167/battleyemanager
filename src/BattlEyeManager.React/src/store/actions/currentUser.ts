import { currentUserService } from '../../services';
import { currentUserConstants } from "../constants";
import { commonActions } from "./commonActions";

export const currentUserActions = {
    getItem
};

function getItem() {
    return commonActions.getItem('', currentUserConstants.SUBJECT, currentUserService);
}