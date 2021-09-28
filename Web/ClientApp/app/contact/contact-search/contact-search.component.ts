import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { ContactAutoComplete } from '../models/contact-auto-complete.interface';
import { TagService } from '../services/tag.service';

@Component({
    selector: 'contact-search',
    templateUrl: './contact-search.component.html',
    styleUrls: ['./contact-search.component.css']
})
export class ContactSearchComponent {
    searchQuery: string = '';
    searchId: any;

    timeOut: number = 300;
    timer: any;

    constructor(private contactService: ContactService, private tagService: TagService) {

    }

    ngOnInit() { }

    getSourceCallback(query: string, syncResults: any, asyncResults: any) {
        // tag autocomplete
        if (query.indexOf('#') === 0) {
            var tagFilter = query.substr(1).toLowerCase();
            var tagSuggestions = new Array<ContactAutoComplete>();

            for (var i = 0; i < this.tagService.allTags.length; i++) {
                var tag = this.tagService.allTags[i];
                var tagSuggestion = '#' + tag.name;
                if (tagFilter === '') {
                    tagSuggestions.push({ id: tag.id, suggestion: tagSuggestion });
                } else if (tag.name.toLowerCase().indexOf(tagFilter) !== -1) {
                    tagSuggestions.push({ id: tag.id, suggestion: tagSuggestion });
                }
            }
            tagSuggestions.sort((a, b) => {
                var aName = a.suggestion.toLowerCase();
                var bName = b.suggestion.toLowerCase();
                return ((aName < bName) ? -1 : ((aName > bName) ? 1 : 0));
            });
            syncResults(tagSuggestions);
            return;
        }

        // contact autocomplete
        if (this.timer) {
            clearTimeout(this.timer);
        }

        var _self = this;

        // throttling request by setTimeout function.
        this.timer = setTimeout(function () {
            _self.contactService.getAutoComplete(query)
                .subscribe(result => {
                    if (typeof asyncResults !== 'undefined') {
                        asyncResults(result);
                    }
                });
        }, this.timeOut);
    }

    itemSelectCallback(obj: any, displayPath: string) {
        if (obj) {
            this.searchQuery = obj[displayPath];
            this.searchId = obj['id'];
            this.searchContact();
        }
    }

    searchContact() {
        if (this.searchQuery.indexOf('#') === 0) {
            this.contactService.searchContactByTag(this.searchId);
        } else {
            this.contactService.searchContact(this.searchQuery);
        }
    }
}
