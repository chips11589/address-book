import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/header/header.component';
import { ContactModule } from './contact/contact.module';
import { ContactComponent } from './contact/contact.component';
import { ContactSearchComponent } from './contact/contact-search/contact-search.component';

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        ContactSearchComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ContactModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'contact', pathMatch: 'full' },
            { path: 'contact/:id', component: ContactComponent },
            { path: '**', redirectTo: 'contact' }
        ])
    ]
})
export class AppModuleShared {
}
