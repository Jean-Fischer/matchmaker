import { createAction, props } from '@ngrx/store';
import { PlayerDto } from 'generated-sources/openapi';

export const addPlayer = createAction(
    '[Players] Add player',
    props<{content : PlayerDto}>()

)


export const loadPlayers = createAction('[Players] Load players');


export const loadPlayersSuccess = createAction('[Players] Load players success', props<{players : PlayerDto[]}>());

export const loadPlayersError = createAction('[Players] Load players error', props<{error : string}>());


