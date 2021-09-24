import { Injectable } from '@angular/core';

import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { HttpClient } from '@angular/common/http';
import { Tag, Contact } from '../models/contact.interface';
import { catchError } from 'rxjs/operators';

@Injectable()
export class TagService extends BaseService {

    baseUrl: string = '';
    allTags: Tag[];

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiURI();
    }

    getTags() {
        return this.http.get<Tag[]>(this.baseUrl + '/tag')
            .pipe(catchError(this.handleError))
    }

    updateTag(tag: Tag) {
        return this.http.put(this.baseUrl + '/tag', tag)
            .pipe(catchError(this.handleError));
    }

    removeTag(tag: Tag) {
        return this.http.delete(this.baseUrl + '/tag?tagId=' + tag.id + '&tagName=' + tag.name)
            .pipe(catchError(this.handleError));
    }

    insertTag(tag: Tag) {
        return this.http.post<Tag>(this.baseUrl + '/tag', tag)
            .pipe(catchError(this.handleError));
    }

    updateContactTags(contact: Contact) {
        return this.http.post(this.baseUrl + '/tag/updateContactTags', contact)
            .pipe(catchError(this.handleError));
    }
}

