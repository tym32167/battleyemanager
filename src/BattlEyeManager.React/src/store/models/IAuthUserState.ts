export interface IAuthUserState {
    loggedIn?: boolean,
    error?: any
}

const globalAny: any = global;
const user = globalAny.localStorage && globalAny.localStorage.getItem('user') && JSON.parse(globalAny.localStorage.getItem('user'));
const initialState = user ? { loggedIn: true, user } : {};

export const AuthUserStateInitial: IAuthUserState = initialState;