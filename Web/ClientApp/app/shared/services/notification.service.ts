import { Injectable } from '@angular/core';
import { Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';
import { HubConnection, TransportType } from "@aspnet/signalr-client";

// Add the RxJS Observable operators we need in this app.
import '../../shared/utils/rxjs-operators';
import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { HttpClient } from '@angular/common/http';
import { AppNotification } from '../models/notification.interface';

@Injectable()
export class NotificationService extends BaseService {
    baseUrl: string = '';
    private _notificationSource = new BehaviorSubject<AppNotification[]>([]);
    notificationObservable$ = this._notificationSource.asObservable();

    private notificationHub: HubConnection;

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getBaseURI();

        this.notificationHub = new HubConnection(
            this.baseUrl + '/notificationHub',
            { transport: TransportType.WebSockets | TransportType.LongPolling });

        this.notificationHub.on(
            "Send",
            data => {
                this._notificationSource.next(data);
            });

        this.notificationHub
            .start()
            .catch(error => console.log(error));
    }

    ngOnDestroy() {
        this.notificationHub.stop();
    }
}

