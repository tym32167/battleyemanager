import { IServer } from "../../models";
import { serverService } from "../../services";
import { SubjectConstants } from "../constants";
import { commonActions } from "./commonActions";
import { ActionBase } from "./core/actionbase";

export const serverActions =
    new ActionBase<IServer>(commonActions, SubjectConstants.SERVER, serverService, '/servers');

