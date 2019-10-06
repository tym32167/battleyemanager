export interface ILineGraphModel {
    labels?: string[]
    dataSets: IDataSet[]
}

export interface IDataSet {
    label?: string,
    data?: number[]
}