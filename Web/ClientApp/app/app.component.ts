import { Component } from '@angular/core';
import { NotificationService } from './shared/services/notification.service';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {

    constructor(public notificationService: NotificationService) { }

    ngOnInit() {
        this.notificationService.startConnection();
    }
}
