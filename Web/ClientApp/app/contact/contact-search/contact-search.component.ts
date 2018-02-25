import { Component } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { ContactAutoComplete } from '../models/contact-auto-complete.interface';
import * as $ from 'jquery';
import { Contact } from '../models/contact.interface';

@Component({
    selector: 'contact-search',
    templateUrl: './contact-search.component.html',
    styleUrls: ['./contact-search.component.css']
})
export class ContactSearchComponent {
    searchQuery: string = '';

    constructor(private contactService: ContactService) {

    }

    ngOnInit() {
        var contactService = this.contactService;
        var _self = this;
        var displayPath = 'suggestion';
        var timeOut = 300;
        var timer: any;

        // todo: make typeahead a global component/directive for reusability
        $('.contact-search .typeahead').typeahead<ContactAutoComplete>({
            minLength: 3,
            highlight: true,
            hint: true
        },
            {
                name: 'autocomplete-dataset',
                display: displayPath,
                source: function (query, syncResults, asyncResults) {
                    if (timer) {
                        clearTimeout(timer);
                    }

                    // Throttling request by setTimeout function.
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
                    _self.searchContact();
                }
            });
    }

    searchContact() {
        this.contactService.searchContact(this.searchQuery);
    }
}
