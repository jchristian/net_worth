import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { BasicHttpService } from '../services/basic-http-service';
import { Transaction } from './transaction';

@Injectable()
export class TransactionService {
    constructor(private httpService: BasicHttpService) { }

    public getTransactions(): Observable<Transaction[]> {
        return this.httpService.get<Transaction[]>('http://localhost:60239/api/transactions');
    }

    public getTransactionsWithTrades(): Observable<Transaction[]> {
        return this.httpService.get<Transaction[]>('http://localhost:60239/api/transactions?includeTrades=true');
    }
}
