import { IUser } from 'src/models';
import { userService } from '../../services';
import { SubjectConstants } from '../constants';
import { commonActions } from "./commonActions";
import { ActionBase } from './core/actionbase';
export const userActions =
    new ActionBase<IUser>(commonActions, SubjectConstants.USER, userService);