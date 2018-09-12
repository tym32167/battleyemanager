import { IOnlineServer } from '../../models';
import { onlineServerService } from '../../services';
import { SubjectConstants } from '../constants';
import { commonActions } from './commonActions';
import { ReadonlyActionBase } from './core/readonlyactionsbase';

export const onlineServerActions =
    new ReadonlyActionBase<IOnlineServer>(commonActions, SubjectConstants.ONLINE_SERVER, onlineServerService);
