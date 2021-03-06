﻿import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { Contact, Tag } from '../models/contact.interface';
import { Subscription } from 'rxjs';
import { TagService } from '../services/tag.service';

@Component({
    selector: 'contact-tag-list',
    templateUrl: './contact-tag-list.component.html',
    styleUrls: ['./contact-tag-list.component.css']
})
export class ContactTagListComponent {
    @Input() contact: Contact;
    @Input() allTags: Tag[];
    @Output() onTagRemoved: EventEmitter<any> = new EventEmitter();
    @Output() onTagEdited: EventEmitter<any> = new EventEmitter();
    newTagName: string;
    subscription: Subscription;

    constructor(private contactService: ContactService, private tagService: TagService) { }

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
        this.tagService.updateTag(tag).subscribe(() => {
            this.onTagEdited.emit(tag);
            tag.isEditing = false;
        });
    }

    insertTag() {
        if (!this.newTagName) {
            return;
        }

        var newTag = { name: this.newTagName };
        this.tagService.insertTag(newTag).subscribe((tag) => {
            var addedGlobalTag = this.allTags.find(globalTag => globalTag.id === tag.id);
            if (typeof addedGlobalTag === 'undefined') {
                this.allTags.push(tag);
            }
        });
        this.newTagName = '';
    }

    deleteTag(tag: Tag) {
        if (confirm('Are you sure you want to delete this record?')) {
            this.tagService.removeTag(tag).subscribe(() => {
                this.allTags.remove(tag);
                this.onTagRemoved.emit(tag);
            });
        }
        return false;
    }

    ngOnDestroy() {
        
    }
}
