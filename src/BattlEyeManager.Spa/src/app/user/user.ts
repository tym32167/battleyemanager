export interface IUser {
    id: string;
    userName: string;
    email: string;
    lastName: string;
    firstName: string;
}

export class User implements IUser {
    id: string;
    userName: string;
    email: string;
    lastName: string;
    firstName: string;
}
