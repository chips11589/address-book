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

    onModalOpen(contact: Contact) {
        for (var i = 0; i < this.allTags.length; i++){
            var tag = this.allTags[i];
            tag.isEditing = false;
            tag.isChecked = false;

            if (contact.tags.length > 0 &&
                contact.tags.map((contactTag) => contactTag.id).indexOf(tag.id) > -1) {
                tag.isChecked = true;
            }
        }
    }

    onModalClosed(contactId: any) {
        
    }

    selectItem(contact: Contact) {
        this.selectedItem = contact;
    }

    onTagRemoved(tag: Tag) {
        for (var i = 0; i < this.contacts.length; i++) {
            let contact = this.contacts[i];

            // untick the checkbox for the tag which has been deleted
            var removingTag = contact.tags.find(contactTag => contactTag.id === tag.id);
            if (typeof removingTag !== 'undefined') {
                contact.tags.remove(removingTag);
            }
        }
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
