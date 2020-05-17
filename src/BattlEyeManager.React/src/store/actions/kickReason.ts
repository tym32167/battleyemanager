import { IKickReason } from "../../models";
import { kickReasonsService } from "../../services";
import { SubjectConstants } from "../constants";
import { commonActions } from "./commonActions";
import { ActionBase } from "./core/actionbase";

export const kickReasonActions =
    new ActionBase<IKickReason>(commonActions, SubjectConstants.KICK_REASON, kickReasonsService, '/kickreasons');