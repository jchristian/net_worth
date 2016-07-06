import { Component, OnInit } from '@angular/core';
import { ROUTER_DIRECTIVES } from '@angular/router';

import { TransactionService } from './transaction.service';
import { Transaction } from './transaction';
import { TransactionImportComponent } from './transaction-import.component';
import { BasicTableComponent } from '../shared/basic-table.component';

@Component({
    templateUrl: 'app/transactions/transaction-list.component.html',
    directives: [ROUTER_DIRECTIVES, TransactionImportComponent, BasicTableComponent],
})
export class TransactionListComponent implements OnInit {
    transactions: Transaction[] = [];
    errorMessage: string;

    constructor(private service: TransactionService) { }

    ngOnInit() {
        this.getTransactions();
    }

    onImportSucceeded() {
        this.getTransactions();
    }

    private getTransactions() {
        this.service.getTransactions()
            .subscribe((transactions: Transaction[]) => this.transactions = transactions,
                       error => this.errorMessage = <any>error);
    }
}
