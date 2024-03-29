import { Observable } from 'rxjs';


export abstract class BaseService {

    constructor() { }

    protected handleError(error: any) {
        var applicationError = error.headers.get('Application-Error');

        // either applicationError in header or model error in body
        if (applicationError) {
            return Observable.throw(applicationError);
        }

        var modelStateErrors: string | null = '';
        var serverError = error;

        if (!serverError.type) {
            for (var key in serverError) {
                if (serverError[key])
                    modelStateErrors += serverError[key] + '\n';
            }
        }

        modelStateErrors = modelStateErrors = '' ? null : modelStateErrors;
        return Observable.throw(modelStateErrors || 'Server error');
    }

    protected getQueryString(queryObj: any) {
        return Object.keys(queryObj).map(function (key) {
            return key + '=' + queryObj[key]
        }).join('&');
    }
}