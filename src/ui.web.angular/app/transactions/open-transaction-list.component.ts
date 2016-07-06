import { Component, OnInit } from '@angular/core';

import { TransactionService } from './transaction.service';
import { Transaction } from './transaction';
import { OpenTransaction } from './open-transaction';

@Component({
    templateUrl: 'app/transactions/open-transaction-list.component.html',
})
export class OpenTransactionListComponent implements OnInit {
    openTransactions: OpenTransaction[] = [];
    errorMessage: string;

    constructor(private service: TransactionService) { }

    ngOnInit() {
        this.service.getTransactionsWithTrades()
            .subscribe((transactions: Transaction[]) => this.processTransactions(transactions),
                       error => this.errorMessage = <any>error);

    }

    private processTransactions(transactions: Transaction[]) {
        this.openTransactions = transactions.map(transaction => {
            let tradedQuantity = transaction.trades.map(trade => trade.quantity).reduce((prev, acc) => acc + prev);
            return <OpenTransaction>{
                transaction: transaction,
                tradedShares: tradedQuantity,
                remainingShares: transaction.shares - tradedQuantity
            };
        }).filter(openTransaction => openTransaction.remainingShares !== 0);
    }
}
