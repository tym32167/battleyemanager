export interface IOnlineBan {
    guidIp: string,
    minutesleft: number,
    reason: string,
    playerName?: string,
    playerComment?: string,
    num: number,
    serverId: number
}