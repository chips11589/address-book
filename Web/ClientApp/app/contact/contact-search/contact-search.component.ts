import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { ContactAutoComplete } from '../models/contact-auto-complete.interface';
import { Contact } from '../models/contact.interface';
import * as $ from 'jquery';
import { TagService } from '../services/tag.service';

@Component({
    selector: 'contact-search',
    templateUrl: './contact-search.component.html',
    styleUrls: ['./contact-search.component.css']
})
export class ContactSearchComponent {
    searchQuery: string = '';
    searchId: any;

    constructor(private contactService: ContactService, private tagService: TagService) {

    }

    ngOnInit() {
        var contactService = this.contactService;
        var _self = this;
        var displayPath = 'suggestion';
        var timeOut = 300;
        var timer: any;

        // todo: make typeahead a global component/directive for reusability
        $('.contact-search .typeahead').typeahead<ContactAutoComplete>({
            minLength: 1,
            highlight: true,
            hint: true
        },
            {
                name: 'autocomplete-dataset',
                display: displayPath,
                source: function (query, syncResults, asyncResults) {
                    // tag autocomplete
                    if (query.indexOf('#') === 0) {
                        var tagFilter = query.substr(1).toLowerCase();
                        var tagSuggestions = new Array<ContactAutoComplete>();

                        for (var i = 0; i < _self.tagService.allTags.length; i++) {
                            var tag = _self.tagService.allTags[i];
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
                    if (timer) {
                        clearTimeout(timer);
                    }

                    // throttling request by setTimeout function.
                    timer = setTimeout(function () {
                        contactService.getAutoComplete(query)
                            .subscribe(result => {
                                if (typeof asyncResults !== 'undefined') {
                                    asyncResults(result);
                                }
                            });
                    }, timeOut);
                }
            }).on('typeahead:selected', function (e, obj, dataSet) {
                if (obj) {
                    _self.searchQuery = obj[displayPath];
                    _self.searchId = obj['id'];
                    _self.searchContact();
                }
            });
    }

    searchContact() {
        if (this.searchQuery.indexOf('#') === 0) {
            this.contactService.searchContactByTag(this.searchId);
        } else {
            this.contactService.searchContact(this.searchQuery);
        }
    }
}
