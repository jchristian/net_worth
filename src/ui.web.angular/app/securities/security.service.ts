import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { Security } from './security';
import { SecurityDescription } from './descriptions/security-description';
import { BasicHttpService } from '../services/basic-http-service';

@Injectable()
export class SecurityService {
    private securities_url = 'http://localhost:60239/api/securities';

    constructor(private httpService: BasicHttpService) { }

    public getSecurities(): Observable<Security[]> {
        return this.httpService.get<Security[]>(this.securities_url);
    }

    public getSecurityDescriptions(securityId: number): Observable<SecurityDescription[]> {
        let url = 'http://localhost:60239/api/security/' + securityId + '/descriptions';
        return this.httpService.get<SecurityDescription[]>(url);
    }
}
