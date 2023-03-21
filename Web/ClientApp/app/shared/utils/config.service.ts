import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable()
export class ConfigService {

    private _apiURI: string;
    private _baseURI: string;

    constructor() {
        this._baseURI = environment.apiEndpoint;
        this._apiURI = this._baseURI + '/api';
    }

    getApiURI() {
        return this._apiURI;
    }

    getBaseURI() {
        return this._baseURI;
    }
}
