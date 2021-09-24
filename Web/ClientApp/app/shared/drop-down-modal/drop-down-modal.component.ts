import { Component, Input, Output, EventEmitter, HostListener, ViewChild, ElementRef } from '@angular/core';
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

    @ViewChild('modal', { static: false }) modal: ElementRef;
    @ViewChild('modalWrapper', { static: false }) modalWrapper: ElementRef;

    isModalOpen: boolean;
    $modal: any;

    ngAfterViewInit() {
        this.$modal = $(this.modal.nativeElement);
    }

    @HostListener('document:click', ['$event'])
    clickout(event) {
        if (!event.target || !this.isModalOpen || event.target.nodeName === 'a') {
            return;
        }

        if (!this.modalWrapper.nativeElement.contains(event.target)) {
            this.$modal.hide();
            this.onClosed.emit();
            this.isModalOpen = false;
        }
    }

    showModal(event: any) {
        this.$modal.show();
        this.onOpen.emit();
        this.isModalOpen = true;
    }
}
