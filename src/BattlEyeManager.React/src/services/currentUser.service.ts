import { IUser } from "../models";
import { IReadonlyService } from "./core";
import { ReadonlyCommonService } from "./core/readonlycommonservice";

const baseUrl = '/api/currentuser/';

export const currentUserService: IReadonlyService<IUser> = new ReadonlyCommonService<IUser>(baseUrl);


