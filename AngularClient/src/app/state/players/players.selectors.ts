import { createSelector } from "@ngrx/store";
import { AppState } from "../app.state";
import { PlayersState } from "./players.reducer";



export const selectPlayersState = (state : AppState) => state.playersState;

export const selectAllPlayers = createSelector(
    selectPlayersState,
    (playerState : PlayersState) => playerState.players
)

