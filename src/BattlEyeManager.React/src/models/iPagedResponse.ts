export interface IPagedResponse<T> {
    skip: number,
    take: number,
    data: T[],
}