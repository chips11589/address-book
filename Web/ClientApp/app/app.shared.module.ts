/// <reference path="../node_modules/@types/typeahead/index.d.ts" />
/// <reference path="shared/utils/array.extension.d.ts" />

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HeaderComponent } from './shared/header/header.component';
import { ContactModule } from './contact/contact.module';
import { ContactComponent } from './contact/contact.component';
import { ContactSearchComponent } from './contact/contact-search/contact-search.component';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from './shared/shared.module';

if (!Array.prototype.remove) {
    Array.prototype.remove = function (o) {
        const index: number = this.indexOf(o);
        if (index !== -1) {
            this.splice(index, 1);
        }
        return this;
    }
}

@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        ContactSearchComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        ContactModule,
        HttpClientModule,
        SharedModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'contact', pathMatch: 'full' },
            { path: 'contact', component: ContactComponent },
            { path: 'contact/:id', component: ContactComponent },
            { path: '**', redirectTo: 'contact' }
        ])
    ]
})
export class AppModuleShared {
}
