import { AuthUserStateInitial, IAuthUserState } from "./IAuthUserState";

export interface IStore {
    auth: IAuthUserState
}

export const StoreInitialState: IStore = {
    auth: AuthUserStateInitial
}

export interface IItemsState<T> extends IItemState<T> {
    items?: T[],
    busy?: boolean
}

export interface IItemState<T> {
    createItemRequest?: T;
    createItem?: T;
    request?: T;
    item?: T;
    deleteItemRequest?: T;
    deleteItem?: T;
    updateItemRequest?: T;
    updateItem?: T;
    error?: any;
}

export interface IRequestAction<T> {
    type: string,
    item?: T,
    items?: T[],
    error?: any
}

