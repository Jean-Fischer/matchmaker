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

@NgModule({
  declarations: [
    AppComponent,
    PlayerCardComponent,
    MatchCreatorComponent,
    MatchCardComponent,
    QueuePageComponent,
    MatchCreatorPageComponent,
    MatchListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ApiModule,
    ToastrModule.forRoot(),
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [{ provide: BASE_PATH, useValue:"https://localhost:7154"}],
  bootstrap: [AppComponent]
})
export class AppModule { }
