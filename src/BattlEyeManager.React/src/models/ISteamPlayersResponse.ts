
export interface ISteamPlayersResponse {
    playerCount: number,
    players: ISteamPlayer[]
}

export interface ISteamPlayer {
    n: number,
    name: string,
    score: number,
    time: string
}