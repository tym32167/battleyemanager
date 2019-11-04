import { IServer } from "src/models";
import { IService } from "./core";
import { CommonService } from "./core/commonService";

const baseUrl = '/api/server/'
const basesimpleUrl = '/api/serverSimple/'
export const serverService: IService<IServer> = new CommonService<IServer>(baseUrl);
export const serverSimpleService: IService<IServer> = new CommonService<IServer>(basesimpleUrl);