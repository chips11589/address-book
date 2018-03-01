import { Component, Input, Output, EventEmitter } from '@angular/core';
import * as $ from 'jquery';

@Component({
    selector: 'drop-down-modal',
    templateUrl: './drop-down-modal.component.html',
    styleUrls: ['./drop-down-modal.component.css']
})
export class DropDownModalComponent {
    @Input() modalId: string;
    @Input() buttonClass: string | null;
    @Input() buttonLabel: string | null;
    @Input() modalClass: string | null;
    @Output() onOpen: EventEmitter<any> = new EventEmitter();
    @Output() onClosed: EventEmitter<any> = new EventEmitter();

    modalWrapperId: string;
    isModalOpen: boolean;
    $modal: any;

    ngOnInit() {
        this.modalWrapperId = 'modal-wrapper' + this.modalId;
    }

    ngAfterViewInit() {
        var _self = this;
        _self.$modal = $('#' + this.modalId);

        $(document).click(function (e) {
            if (!e.target || !_self.isModalOpen || e.target.localName === 'a') {
                return;
            }

            var wrapperIdSelector = '#' + _self.modalWrapperId;
            var container = $(wrapperIdSelector);

            // if the target of the click isn't the container nor a descendant of the container
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                _self.$modal.hide();
                _self.onClosed.emit();
                _self.isModalOpen = false;
            }
        });
    }

    showModal(event: any) {
        this.$modal.show();
        this.onOpen.emit();
        this.isModalOpen = true;
    }
}
