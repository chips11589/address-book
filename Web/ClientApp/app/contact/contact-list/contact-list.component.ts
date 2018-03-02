﻿import { Component } from '@angular/core';
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
    originalTags: Tag[];

    constructor(private contactService: ContactService, private tagService: TagService) { }

    ngOnInit() {
        this.subscription = this.contactService.contactObservable$.subscribe(contacts => {
            this.contacts = contacts;
        });

        this.tagService.getTags().subscribe(tags => {
            this.allTags = tags;
            this.tagService.allTags = tags;
        });
    }

    onModalOpen(contact: Contact) {
        for (var i = 0; i < this.allTags.length; i++) {
            var tag = this.allTags[i];
            tag.isEditing = false;
            tag.isChecked = false;

            if (contact.tags.length > 0 &&
                contact.tags.map((contactTag) => contactTag.id).indexOf(tag.id) > -1) {
                tag.isChecked = true;
            }
        }
        this.originalTags = JSON.parse(JSON.stringify(this.allTags));
    }

    onModalClosed(contact: Contact) {
        var editingTags = this.allTags.filter((item) => item.isEditing);
        for (var i = 0; i < editingTags.length; i++) {
            var editingTag = editingTags[i];
            var originalTag = this.originalTags.find(item => item.id === editingTag.id);
            if (typeof originalTag !== 'undefined') {
                // reset name of tags being edited
                editingTag.name = originalTag.name;
            }
            editingTag.isEditing = false;
        }

        // update new tags for selected contact
        this.tagService.updateContactTags(contact).subscribe();
    }

    selectItem(contact: Contact) {
        this.selectedItem = contact;
    }

    onTagEdited(tag: Tag) {
        for (var i = 0; i < this.contacts.length; i++) {
            let contact = this.contacts[i];

            // update new tag name for existing contact's tags
            var updatedTag = contact.tags.find(contactTag => contactTag.id === tag.id);
            if (typeof updatedTag !== 'undefined') {
                updatedTag.name = tag.name;
            }
        }
    }

    onTagRemoved(tag: Tag) {
        for (var i = 0; i < this.contacts.length; i++) {
            let contact = this.contacts[i];

            // untick the checkbox for the tag which has been deleted
            var removedTag = contact.tags.find(contactTag => contactTag.id === tag.id);
            if (typeof removedTag !== 'undefined') {
                contact.tags.remove(removedTag);
            }
        }
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
