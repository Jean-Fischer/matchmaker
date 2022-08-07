import { PlayerDto } from "generated-sources/openapi";
import { createReducer, on } from '@ngrx/store';
import { addPlayer, loadPlayers, loadPlayersError, loadPlayersSuccess } from "./players.actions"; './players.actions';


export interface PlayersState {
    players: PlayerDto[];
    error: string | null;
    status: 'pending' | 'loading' | 'error' | 'success'

}

export const initialPlayersState: PlayersState = {
    error: null,
    players: [],
    status: 'pending'
}


export const playersReducer = createReducer(
    initialPlayersState,

    on(addPlayer, (state, { content }) => ({ ...state, players: [...state.players, content] })),
    // Trigger loading the todos
    on(loadPlayers, (state) => ({ ...state, status: 'loading' })),

    on(loadPlayersSuccess, (state, { players }) => ({
        ...state,
        players: players,
        error: null,
        status: 'success',
    })),
    on(loadPlayersError, (state, { error }) => ({
        ...state,
        error: error,
        status: 'error',
    }))

)
