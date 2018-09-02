import { currentUserService } from '../../services';
import { SubjectConstants } from '../constants';
import { commonActions } from "./commonActions";

export const currentUserActions = {
    getItem
};

function getItem() {
    return commonActions.getItem('', SubjectConstants.CURRENT_USER, currentUserService);
}