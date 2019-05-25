import { Injectable } from '@angular/core';
import { Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';
import * as SignalR from "@aspnet/signalr";

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

    private hubConnection: SignalR.HubConnection | undefined;

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getBaseURI();
    }

    public startConnection = () => {
        if (typeof document === 'undefined') {
            return;
        }

        if (this.hubConnection && this.hubConnection.state == SignalR.HubConnectionState.Connected) {
            return;
        }

        this.hubConnection = new SignalR.HubConnectionBuilder()
            .withUrl(this.baseUrl + '/notificationHub')
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))

        this.hubConnection.on(
            "Send",
            (data: AppNotification[]) => {
                this._notificationSource.next(data);
            });
    }
}

