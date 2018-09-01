import { AuthUserStateInitial, IAuthUserState } from "./IAuthUserState";

export interface IStore {
    auth: IAuthUserState
}

export const StoreInitialState: IStore = {
    auth: AuthUserStateInitial
}