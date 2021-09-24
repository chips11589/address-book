import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { Contact } from '../models/contact.interface';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';

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
        return this.http.get<Contact>(this.baseUrl + '/contact/getContact?id=' + id)
            .pipe(catchError(this.handleError));
    }

    getAutoComplete(searchQuery: string) {
        return this.http.get(this.baseUrl + '/contact/getAutoComplete?searchQuery=' + searchQuery)
            .pipe(catchError(this.handleError));
    }
}

