import { Injectable } from '@angular/core';
import { Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { BehaviorSubject } from 'rxjs/Rx';

// Add the RxJS Observable operators we need in this app.
import '../../shared/utils/rxjs-operators';
import { BaseService } from '../../shared/services/base.service';
import { ConfigService } from '../../shared/utils/config.service';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class TagService extends BaseService {

    baseUrl: string = '';

    constructor(private http: HttpClient, private configService: ConfigService) {
        super();
        this.baseUrl = configService.getApiURI();
    }

    getTags() {
        return this.http.get(this.baseUrl + '/tag')
            .catch(this.handleError);
    }
}

