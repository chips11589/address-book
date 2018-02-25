import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { routing } from './contact.routing';
import { ContactComponent } from './contact.component';
import { ContactService } from './services/contact.service';
import { ConfigService } from '../shared/utils/config.service';
import { ContactListComponent } from './contact-list/contact-list.component';
import { ContactDetailsComponent } from './contact-details/contact-details.component';

@NgModule({
    imports: [
        CommonModule, FormsModule, routing
    ],
    declarations: [ContactComponent, ContactListComponent, ContactDetailsComponent],
    providers: [ContactService, ConfigService]
})
export class ContactModule { }
