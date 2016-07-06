import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BasicHttpService } from '../services/basic-http-service';
import { Lot } from './lot';

@Injectable()
export class LotService {

    constructor(private httpService: BasicHttpService) { }

    public getLots(): Observable<Lot[]> {
        return this.httpService.get<Lot[]>('http://localhost:60239/api/lots');
    }
}
