export * from './commonService';
export * from './readonlycommonservice';

export interface IService<T> extends IReadonlyService<T> {
    addItem: (item: T) => Promise<T>,
    deleteItem: (id: string | number) => Promise<any>,
    getItem: (id: string | number) => Promise<T>,
    updateItem: (item: T) => Promise<T>
}


export interface IReadonlyService<T> {
    getItem: (id: string | number) => Promise<T>,
    getItems: () => Promise<T[]>,
}