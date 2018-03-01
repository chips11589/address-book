import { Component, Input, Output, EventEmitter } from '@angular/core';
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
    @Output() onTagRemoved: EventEmitter<any> = new EventEmitter();
    newTagName: string;
    subscription: Subscription;

    constructor(private contactService: ContactService) { }

    ngOnInit() {
        
    }

    editTag(tag: Tag) {
        tag.isEditing = true;
        return false;
    }

    private removeContactTag(tag: Tag) {
        var removingTag = this.contact.tags.find(contactTag => contactTag.id === tag.id);
        if (typeof removingTag !== 'undefined') {
            this.contact.tags.remove(removingTag);
        }
    }

    onCheckChanged(tag: Tag, value: boolean) {
        if (value) {
            this.contact.tags.push(tag);
        } else {
            this.removeContactTag(tag);
        }
    }

    completeEdit(tag: Tag) {
        tag.isEditing = false;
    }

    insertTag() {
        var time = new Date().getTime();
        this.allTags.push({ id: '-' + time, name: this.newTagName });
        this.newTagName = '';
    }

    deleteTag(tag: Tag) {
        if (confirm('Are you sure you want to delete this record?')) {
            this.allTags.remove(tag);
            this.onTagRemoved.emit(tag);
        }
        return false;
    }

    ngOnDestroy() {
        
    }
}
