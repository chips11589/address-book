import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppModuleShared } from './app.shared.module';
import { AppComponent } from './app.component';

// javascript libraries for browser only
import 'typeahead.js';
import './shared/utils/array.extension.js';

@NgModule({
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        AppModuleShared
    ],
    providers: []
})

export class AppModule { }
