import { Injectable } from '@angular/core';

@Injectable()
export class ConfigService {

    private _apiURI: string;
    private _baseURI: string;

    constructor() {
        this._baseURI = 'http://localhost:30232';
        this._apiURI = this._baseURI + '/api';
    }

    getApiURI() {
        return this._apiURI;
    }

    getBaseURI() {
        return this._baseURI;
    }
}
