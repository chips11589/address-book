import { Injectable } from '@angular/core';
import * as SignalR from "@microsoft/signalr";
import { BehaviorSubject } from 'rxjs';

import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { TagChangedNotification } from '../models/notification.interface';

@Injectable()
export class NotificationService extends BaseService {
    baseUrl: string = '';
    private _notificationSource = new BehaviorSubject<TagChangedNotification>(null);
    notificationObservable$ = this._notificationSource.asObservable();

    private hubConnection: SignalR.HubConnection | undefined;

    constructor(configService: ConfigService) {
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
            .withAutomaticReconnect()
            .build();

        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))

        this.hubConnection.on(
            'HandleTagChanged',
            (data: TagChangedNotification) => {
                this._notificationSource.next(data);
            });
    }
}

