import { Input, ElementRef, Directive } from '@angular/core';
import * as $ from 'jquery';

@Directive({
    selector: '[typeahead]'
})
export class TypeaheadDirective {
    @Input() displayPath: string;
    @Input() getSourceCallback: any;
    @Input() itemSelectCallback: any;

    elementRef: ElementRef;

    constructor(el: ElementRef) {
        this.elementRef = el;
    }

    ngAfterViewInit() {
        var _self = this;

        $(this.elementRef.nativeElement).typeahead<any>({
            minLength: 1,
            highlight: true,
            hint: true
        },
            {
                name: 'autocomplete-dataset',
                display: this.displayPath,
                source: function (query, syncResults, asyncResults) {
                    _self.getSourceCallback(query, syncResults, asyncResults);
                }
            }).on('typeahead:selected', function (e, obj, dataSet) {
                _self.itemSelectCallback(obj, _self.displayPath)
            });
    }
}
