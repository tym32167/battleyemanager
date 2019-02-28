import { IOnlineServer } from "./ionlineserver";

export interface IServer extends IOnlineServer {
    password: string,
    welcomeFeatureEnabled: boolean,
    welcomeFeatureTemplate: string,
    welcomeFeatureEmptyTemplate: string
}

