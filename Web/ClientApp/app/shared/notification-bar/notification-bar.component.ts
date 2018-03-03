import { Component, Input, Output, EventEmitter, ViewChild, ElementRef } from '@angular/core';
import * as $ from 'jquery';
import { NotificationService } from '../services/notification.service';
import { Subscription } from 'rxjs';
import { AppNotification } from '../models/notification.interface';

@Component({
    selector: 'notification-bar',
    templateUrl: './notification-bar.component.html',
    styleUrls: ['./notification-bar.component.css']
})
export class NotificationBarComponent {
    @Input() notificationType: string;
    @ViewChild('modal') modal: ElementRef;

    notifications: AppNotification[];
    private subscription : Subscription;

    isModalOpen: boolean = false;
    $modal: JQuery<HTMLElement>;

    constructor(private notificationService: NotificationService) { }

    ngOnInit() {
        this.subscription = this.notificationService.notificationObservable$
            .subscribe(notifications => {
                if (notifications.length === 0) {
                    return;
                }

                this.notifications = notifications
                this.$modal.show(() => {
                    this.isModalOpen = true;
                }).delay(3000).hide(() => {
                    this.isModalOpen = false;
                });
            });
    }

    ngAfterViewInit() {
        this.$modal = $(this.modal.nativeElement);
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    showModal(event: any) {

    }
}
