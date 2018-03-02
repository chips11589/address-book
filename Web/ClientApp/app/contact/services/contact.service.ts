import { Injectable } from '@angular/core';
import { Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

// Add the RxJS Observable operators we need in this app.
import '../../shared/utils/rxjs-operators';
import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { Contact } from '../models/contact.interface';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class ContactService extends BaseService {

    baseUrl: string = '';
    private _contactSource = new BehaviorSubject<Contact[]>([]);
    contactObservable$ = this._contactSource.asObservable();

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiURI();
    }

    searchContact(searchQuery: string) {
        return this.http.get(this.baseUrl + '/contact?searchQuery=' + searchQuery)
            .subscribe(result => {
                this._contactSource.next(result as Contact[]);
            }, this.handleError);
    }

    searchContactByTag(tagId: any   ) {
        return this.http.get(this.baseUrl + '/contact/getContactsByTag?tagId=' + tagId)
            .subscribe(result => {
                this._contactSource.next(result as Contact[]);
            }, this.handleError);
    }

    getContact(id: any) {
        return this.http.get(this.baseUrl + '/contact/getContact?id=' + id)
            .catch(this.handleError);
    }

    getAutoComplete(searchQuery: string) {
        return this.http.get(this.baseUrl + '/contact/getAutoComplete?searchQuery=' + searchQuery)
            .catch(this.handleError);
    }
}

