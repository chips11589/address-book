import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';

@Component({
    selector: 'contact-search',
    templateUrl: './contact-search.component.html',
    styleUrls: ['./contact-search.component.css']
})
export class ContactSearchComponent {
    searchQuery: string = '';

    constructor(private contactService: ContactService) { }

    searchContact() {
        this.contactService.searchContact(this.searchQuery);
    }
}
