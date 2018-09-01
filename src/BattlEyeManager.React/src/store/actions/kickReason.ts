import { IKickReason } from "../../models";
import { kickReasonsService } from "../../services";
import { kickReasonConstants } from "../constants";
import { commonActions } from "./commonActions";
import { ActionBase } from "./core/actionbase";

export const kickReasonActions =
    new ActionBase<IKickReason>(commonActions, kickReasonConstants.SUBJECT, kickReasonsService);