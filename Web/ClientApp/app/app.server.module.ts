import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppModuleShared } from './app.shared.module';
import { AppComponent } from './app.component';

@NgModule({
    bootstrap: [AppComponent],
    imports: [
        ServerModule,
        AppModuleShared
    ]
})
export class AppModule { }
