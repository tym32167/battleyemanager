import { IUser } from 'src/models';
import { userService } from '../../services';
import { userConstants } from "../constants";
import { commonActions } from "./commonActions";
import { ActionBase } from './core/actionbase';
export const userActions = new ActionBase<IUser>(commonActions, userConstants.SUBJECT, userService);