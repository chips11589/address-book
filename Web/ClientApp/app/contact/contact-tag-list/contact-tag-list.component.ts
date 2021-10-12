import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { Contact, Tag } from '../models/entities.interface';
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

    constructor(private tagService: TagService) { }

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
        this.tagService.updateTag({ tag: tag }).subscribe(() => {
            this.onTagEdited.emit(tag);
            tag.isEditing = false;
        });
    }

    insertTag() {
        if (!this.newTagName) {
            return;
        }

        var newTag: Tag = { name: this.newTagName };
        this.tagService.insertTag({ tag: newTag }).subscribe((id) => {
            var existingTag = this.allTags.find(globalTag => globalTag.id === id);
            if (typeof existingTag === 'undefined') {
                newTag.id = id;
                this.allTags.push(newTag);
            }
        });
        this.newTagName = '';
    }

    deleteTag(tag: Tag) {
        if (confirm('Are you sure you want to delete this record?')) {
            this.tagService.removeTag(tag.id).subscribe(() => {
                this.allTags.remove(tag);
                this.onTagRemoved.emit(tag);
            });
        }
        return false;
    }

    ngOnDestroy() {
        
    }
}
