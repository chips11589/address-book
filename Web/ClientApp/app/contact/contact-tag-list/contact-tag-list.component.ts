import { Component, Input } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { Contact, Tag } from '../models/contact.interface';
import { Subscription } from 'rxjs';

@Component({
    selector: 'contact-tag-list',
    templateUrl: './contact-tag-list.component.html',
    styleUrls: ['./contact-tag-list.component.css']
})
export class ContactTagListComponent {
    @Input() contact: Contact;
    @Input() allTags: Tag[];
    newTagName: string;
    subscription: Subscription;

    constructor(private contactService: ContactService) { }

    ngOnInit() {
        
    }

    editTag(tag: Tag) {
        tag.isEditing = true;
        return false;
    }

    completeEdit(tag: Tag) {
        // todo: submit edit request
        tag.isEditing = false;
    }

    deleteTag(tag: Tag) {
        if (confirm('Are you sure you want to delete this record?')) {
            // todo: remove tag from database
        }
        return false;
    }

    ngOnDestroy() {
        
    }
}
