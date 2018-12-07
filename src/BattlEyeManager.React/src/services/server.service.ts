import { IServer } from "src/models";
import { IService } from "./core";
import { CommonService } from "./core/commonService";

const baseUrl = '/api/server/'
export const serverService: IService<IServer> = new CommonService<IServer>(baseUrl);