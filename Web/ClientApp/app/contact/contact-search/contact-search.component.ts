import { Component } from '@angular/core';
import { debug } from 'console';
import { ContactAutoComplete } from '../models/contact-auto-complete.interface';
import { GetContactsQuery } from '../models/queries.interface';
import { ContactService } from '../services/contact.service';
import { TagService } from '../services/tag.service';

@Component({
    selector: 'contact-search',
    templateUrl: './contact-search.component.html',
    styleUrls: ['./contact-search.component.css']
})
export class ContactSearchComponent {
    query: GetContactsQuery;

    timeOut: number = 300;
    timer: any;

    constructor(private contactService: ContactService, private tagService: TagService) {

    }

    ngOnInit() {
        this.searchContact();
    }

    getSourceCallback(query: string, syncResults: any, asyncResults: any) {
        // tag autocomplete
        if (query.indexOf('#') === 0) {
            var tagFilter = query.substring(1).toLowerCase();
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
            _self.contactService.getAutoComplete({
                searchQuery: query
            })
                .subscribe(result => {
                    if (typeof asyncResults !== 'undefined') {
                        asyncResults(result);
                    }
                });
        }, this.timeOut);
    }

    itemSelectCallback(obj: any, displayPath: string) {
        if (obj) {
            if (obj[displayPath].indexOf('#') === 0) {
                this.query = { tagId: obj['id'] };
            }
            else {
                this.query = { searchQuery: obj[displayPath] };
            }
            this.searchContact();
        }
    }

    searchContact() {
        this.contactService.searchContact(this.query);
    }

    searchContactWithQuery(query) {
        this.contactService.searchContact({
            searchQuery: query
        });
    }
}
