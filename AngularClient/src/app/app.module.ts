import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ApiModule, BASE_PATH } from 'generated-sources/openapi';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PlayerCardComponent } from './player-card/player-card.component';
import { MatchCreatorComponent } from './match-creator/match-creator.component';
import { MatchCardComponent } from './match-card/match-card.component';

@NgModule({
  declarations: [
    AppComponent,
    PlayerCardComponent,
    MatchCreatorComponent,
    MatchCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule,
    HttpClientModule
  ],
  providers: [{ provide: BASE_PATH, useValue:"https://localhost:7154"}],
  bootstrap: [AppComponent]
})
export class AppModule { }
