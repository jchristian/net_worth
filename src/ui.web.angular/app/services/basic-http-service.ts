import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class BasicHttpService {
    constructor(private http: Http) { }

    public get<T>(url: string): Observable<T> {
        return this.http.get(url, { headers: this.getApiHeaders() })
                        .map((response: Response) => <T>response.json())
                        .do(data => console.log(data))
                        .catch(this.handleError);
    }

    private getApiHeaders(): Headers {
        return new Headers([ 'Accept', 'application/json' ]);
    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server Error');
    }
}
