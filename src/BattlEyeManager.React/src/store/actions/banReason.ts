import { IBanReason } from "../../models";
import { banReasonsService } from "../../services";
import { banReasonConstants } from "../constants";
import { commonActions } from "./commonActions";
import { ActionBase } from "./core/actionbase";

export const banReasonActions =
    new ActionBase<IBanReason>(commonActions, banReasonConstants.SUBJECT, banReasonsService);