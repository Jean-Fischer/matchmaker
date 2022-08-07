import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ApiModule, BASE_PATH } from 'generated-sources/openapi';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PlayerCardComponent } from './player-card/player-card.component';
import { MatchCreatorComponent } from './match-creator/match-creator.component';
import { MatchCardComponent } from './match-card/match-card.component';
import { ReactiveFormsModule } from '@angular/forms';
import { QueuePageComponent } from './queue-page/queue-page.component';
import { MatchCreatorPageComponent } from './match-creator-page/match-creator-page.component';
import { MatchListComponent } from './match-list/match-list.component';
import { ToastrModule } from 'ngx-toastr';
import { GrpcCoreModule } from '@ngx-grpc/core';
import { GrpcWebClientModule } from '@ngx-grpc/grpc-web-client';
import { MatchQueueCardComponent } from './match-queue-card/match-queue-card.component';
import { MatchHub } from './signalR/matchHub';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EasyDataComponent } from './easydata/easydata.component';
import { GraphQLModule } from './graphql.module';
import { GraphQLService } from './services/graphQL/graphQL.service';
import { PlayerManagementComponent } from './player-management/player-management.component';
import { playersReducer } from './state/players/players.reducer';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { EffectsModule } from '@ngrx/effects';
import { PlayersEffects } from './state/players/players.effects';

@NgModule({
  declarations: [
    AppComponent,
    PlayerCardComponent,
    PlayerManagementComponent,
    MatchCreatorComponent,
    MatchCardComponent,
    QueuePageComponent,
    MatchCreatorPageComponent,
    MatchListComponent,
    MatchQueueCardComponent,
    EasyDataComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule,
    ToastrModule.forRoot(),
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    GrpcCoreModule.forRoot(),
    GrpcWebClientModule.forRoot({
      settings: { host: 'https://localhost:7154' }
    }),
    GraphQLModule,
    StoreModule.forRoot({players: playersReducer}),
    StoreDevtoolsModule.instrument({maxAge:25}),
    EffectsModule.forRoot([PlayersEffects])
  ],
  providers: [{ provide: BASE_PATH, useValue: "https://localhost:7154" }, MatchHub, GraphQLService],
  bootstrap: [AppComponent]
})
export class AppModule { }
