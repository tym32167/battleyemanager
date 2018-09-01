import { IServer } from "../../models";
import { serverService } from "../../services";
import { serverConstants } from "../constants";
import { commonActions } from "./commonActions";
import { ActionBase } from "./core/actionbase";

export const serverActions =
    new ActionBase<IServer>(commonActions, serverConstants.SUBJECT, serverService);

