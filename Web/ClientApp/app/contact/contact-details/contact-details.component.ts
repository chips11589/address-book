import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { Contact } from '../models/contact.interface';
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
            var id = params['id']; // (+) converts string 'id' to a number

            this.contactService.getContact(id)
                .subscribe(contact => {
                    this.contact = contact;
                });
        });
    }
}
