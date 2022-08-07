import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { Store } from "@ngrx/store";
import { PlayerService } from "generated-sources/openapi";
import { catchError, map, of, switchMap } from "rxjs";
import { AppState } from "../app.state";
import { loadPlayers, loadPlayersError, loadPlayersSuccess } from "./players.actions";



@Injectable()
export class PlayersEffects {


  constructor(
    private actions$ : Actions,
    private store: Store<AppState>,
    private playerService: PlayerService

  ) { }

  loadTodos$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadPlayers),
      switchMap(() =>
        // Call the getTodos method, convert it to an observable
        this.playerService.apiPlayerGet().pipe(
          // Take the returned value and return a new success action containing the todos
          map((player) => loadPlayersSuccess({ players: player })),
          // Or... if it errors return a new failure action containing the error
          catchError((error) => of(loadPlayersError({ error })))
        )
      )
    )
  );

}
