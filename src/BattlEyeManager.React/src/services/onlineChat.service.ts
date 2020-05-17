import { IChatMessage } from "src/models";
import { CommonService } from "./core/commonService";


const baseUrl = '/api/onlineserver/'

const service = new CommonService<IChatMessage>();

export const onlineChatService = {
    addItem: (serverId: any, item: any) => service.addItemBy(baseUrl + serverId + '/chat/', item),
    getItems: (serverId: any) => service.getItemsBy(baseUrl + serverId + '/chat/'),
};
