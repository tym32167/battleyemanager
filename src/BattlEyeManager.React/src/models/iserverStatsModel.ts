export interface IServerStatsModel {
    labels?: string[]
    dataSets: IServerStatsDataSet[]
}

export interface IServerStatsDataSet {
    label?: string,
    data?: number[]
}