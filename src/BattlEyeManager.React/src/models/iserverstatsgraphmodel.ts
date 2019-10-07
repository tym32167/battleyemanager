export interface IServerStatsGraphModel {
    dates?: Date[]
    dataSets: IDataSet[]
}

export interface IDataSet {
    label?: string,
    data?: number[]
}