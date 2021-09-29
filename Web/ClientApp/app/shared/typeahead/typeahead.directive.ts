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
    readonly inputDelay: number = 200;
    timeOut: any;

    constructor(el: ElementRef) {
        this.elementRef = el;
    }

    private resetTimeout = () => {
        if (this.timeOut) {
            clearTimeout(this.timeOut);
        }
    }

    ngAfterViewInit() {
        var self = this;
        var nativeElement = this.elementRef.nativeElement;

        $(nativeElement).parent().prepend('<img class="typeahead-spinner" src="/assets/images/Spinner-1s-50px.gif" />');

        $(nativeElement).typeahead<any>({
            minLength: 1,
            highlight: true,
            hint: true
        },
            {
                name: 'autocomplete-dataset',
                display: this.displayPath,
                source: function (query, syncResults, asyncResults) {
                    self.getSourceCallback(query, syncResults, asyncResults);
                }
            }).on('typeahead:selected', function (e, obj, dataSet) {
                self.itemSelectCallback(obj, self.displayPath)
            }).on('typeahead:asyncrequest', function () {
                self.resetTimeout();
                self.timeOut = setTimeout(() => {
                    $(nativeElement).addClass('text-hidden');
                    $('.typeahead-spinner').show();
                }, self.inputDelay);
            }).on('typeahead:asynccancel typeahead:asyncreceive', function () {
                self.resetTimeout();
                $(nativeElement).removeClass('text-hidden');
                $('.typeahead-spinner').hide();
            }.bind(this));
    }
}
