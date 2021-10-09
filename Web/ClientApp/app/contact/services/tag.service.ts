import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { CreateTagCommand, DeleteTagCommand, UpdateContactTagsCommand, UpdateTagCommand } from '../models/commands.interface';
import { Tag } from '../models/entities.interface';


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

    updateTag(command: UpdateTagCommand) {
        return this.http.put(this.baseUrl + '/tag', command)
            .pipe(catchError(this.handleError));
    }

    removeTag(id: string) {
        return this.http.delete(this.baseUrl + '/tag?id=' + id)
            .pipe(catchError(this.handleError));
    }

    insertTag(command: CreateTagCommand) {
        return this.http.post<string>(this.baseUrl + '/tag', command)
            .pipe(catchError(this.handleError));
    }

    updateContactTags(command: UpdateContactTagsCommand) {
        return this.http.post(this.baseUrl + '/tag/updateContactTags', command)
            .pipe(catchError(this.handleError));
    }
}

