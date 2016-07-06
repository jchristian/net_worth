import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BasicHttpService } from '../services/basic-http-service';
import { Trade } from './trade';

@Injectable()
export class TradeService {

    constructor(private httpService: BasicHttpService) { }

    public getTrades(): Observable<Trade[]> {
        return this.httpService.get<Trade[]>('http://localhost:60239/api/trades');
    }
}
