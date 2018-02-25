import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { ContactAutoComplete } from '../models/contact-auto-complete.interface';
import * as $ from 'jquery';
import { Contact } from '../models/contact.interface';

@Component({
    selector: 'contact-list',
    templateUrl: './contact-list.component.html',
    styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent {
    contacts: Contact[];

    constructor(private contactService: ContactService) { }

    ngOnInit() {
        this.contactService.contactObservable$.subscribe(contacts => {
            this.contacts = contacts;
        });
    }
}
