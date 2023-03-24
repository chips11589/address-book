import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { Contact } from '../models/entities.interface';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { GetContactAutoCompleteQuery, GetContactsQuery } from '../models/queries.interface';

@Injectable()
export class ContactService extends BaseService {

    baseUrl: string = '';
    private _contactSource = new BehaviorSubject<Contact[]>([]);
    contactObservable$ = this._contactSource.asObservable();

    private _loadingSource = new BehaviorSubject<boolean>(false);
    loadingObservable$ = this._loadingSource.asObservable();

    constructor(private http: HttpClient, configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiURI();
    }

    searchContact(query: GetContactsQuery) {
        this._loadingSource.next(true);

        return this.http.get(this.baseUrl + '/contact?' + (query ? this.getQueryString(query) : ''))
            .subscribe(result => {
                this._contactSource.next(result as Contact[]);
                this._loadingSource.next(false);
            }, this.handleError);
    }

    getContact(id: any) {
        return this.http.get<Contact>(this.baseUrl + '/contact/' + id)
            .pipe(catchError(this.handleError));
    }

    getAutoComplete(query: GetContactAutoCompleteQuery) {
        return this.http.get(this.baseUrl + '/contact/getAutoComplete?' + this.getQueryString(query))
            .pipe(catchError(this.handleError));
    }
}

