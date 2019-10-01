import { IServerModeratorItem } from "src/models";
import { CommonService } from "./core/commonService";


const baseUrl = '/api/servermoderator/'

const service = new CommonService<IServerModeratorItem>();

export const serverModeratorService = {
    getItems: (userId: string) => service.getItemsBy(baseUrl + userId),
};