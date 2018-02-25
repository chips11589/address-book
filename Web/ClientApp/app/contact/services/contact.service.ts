import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

// Add the RxJS Observable operators we need in this app.
import '../../shared/utils/rxjs-operators';
import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { Contact } from '../models/contact.interface';

@Injectable()
export class ContactService extends BaseService {

    baseUrl: string = '';
    contacts: Contact[];

    constructor(private http: Http, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiURI();


    }

    searchContact(searchQuery: string) {
        this.http.get(this.baseUrl + '/contact/getAutoComplete?searchQuery=' + searchQuery)
            .subscribe(result => {

            }, this.handleError);
    }
}

