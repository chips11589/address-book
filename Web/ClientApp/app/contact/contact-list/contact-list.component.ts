import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import * as $ from 'jquery';
import { Contact, Tag } from '../models/contact.interface';
import { Subscription } from 'rxjs';
import { TagService } from '../services/tag.service';

@Component({
    selector: 'contact-list',
    templateUrl: './contact-list.component.html',
    styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent {
    contacts: Contact[];
    subscription: Subscription;
    selectedItem: Contact;
    allTags: Tag[];

    constructor(private contactService: ContactService, private tagService: TagService) { }

    ngOnInit() {
        this.subscription = this.contactService.contactObservable$.subscribe(contacts => {
            this.contacts = contacts;
        });

        this.tagService.getTags().subscribe(tags => {
            this.allTags = tags;
        });
    }

    selectItem(contact: Contact) {
        this.selectedItem = contact;
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
