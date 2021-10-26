import { Component, Input, ViewChild, ElementRef } from '@angular/core';
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
    @ViewChild('modal', { static: false }) modal: ElementRef;

    readonly maxNotifications: number = 5;

    notifications: AppNotification[] = [];
    private subscription : Subscription;

    isModalOpen: boolean = false;
    $modal: JQuery<HTMLElement>;
    timeOut: number = 1000;
    timer: any;

    constructor(private notificationService: NotificationService) { }

    ngOnInit() {
        this.subscription = this.notificationService.notificationObservable$
            .subscribe(notification => {
                var _self = this;

                if (notification == null) {
                    return;
                }

                this.notifications.unshift(notification);
                this.notifications.splice(_self.maxNotifications);

                if (this.timer) {
                    clearTimeout(this.timer);
                }

                this.timer = setTimeout(function () {
                    _self.$modal.show(() => {
                        _self.isModalOpen = true;
                    }).delay(3000).hide(() => {
                        _self.isModalOpen = false;
                    });
                }, this.timeOut);
            });
    }

    ngAfterViewInit() {
        this.$modal = $(this.modal.nativeElement);
    }

    ngOnDestroy() {
        this.subscription.unsubscribe();
    }
}
