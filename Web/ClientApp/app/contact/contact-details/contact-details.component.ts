import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { Contact } from '../models/entities.interface';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'contact-details',
    templateUrl: './contact-details.component.html',
    styleUrls: ['./contact-details.component.css']
})
export class ContactDetailsComponent {
    contact: Contact;

    constructor(private contactService: ContactService, private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            var id = params['id'];

            if (typeof id !== 'undefined') {
                this.contactService.getContact(id)
                    .subscribe(contact => {
                        this.contact = contact;
                    });
            }
        });
    }
}
