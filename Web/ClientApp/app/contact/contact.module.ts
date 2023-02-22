import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ContactComponent } from './contact.component';
import { ContactService } from './services/contact.service';
import { ContactListComponent } from './contact-list/contact-list.component';
import { ContactDetailsComponent } from './contact-details/contact-details.component';
import { ContactTagListComponent } from './contact-tag-list/contact-tag-list.component';
import { SharedModule } from '../shared/shared.module';
import { TagService } from './services/tag.service';
import { NotificationService } from '../shared/services/notification.service';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        CommonModule, FormsModule, RouterModule, SharedModule
    ],
    declarations: [ContactComponent, ContactListComponent, ContactDetailsComponent, ContactTagListComponent],
    providers: [ContactService, TagService, NotificationService]
})
export class ContactModule { }
