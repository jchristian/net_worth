System.register(['@angular/core', './transaction.service'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, transaction_service_1;
    var OpenTransactionListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (transaction_service_1_1) {
                transaction_service_1 = transaction_service_1_1;
            }],
        execute: function() {
            OpenTransactionListComponent = (function () {
                function OpenTransactionListComponent(service) {
                    this.service = service;
                    this.openTransactions = [];
                }
                OpenTransactionListComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.service.getTransactionsWithTrades()
                        .subscribe(function (transactions) { return _this.processTransactions(transactions); }, function (error) { return _this.errorMessage = error; });
                };
                OpenTransactionListComponent.prototype.processTransactions = function (transactions) {
                    this.openTransactions = transactions.map(function (transaction) {
                        var tradedQuantity = transaction.trades.map(function (trade) { return trade.quantity; }).reduce(function (prev, acc) { return acc + prev; });
                        return {
                            transaction: transaction,
                            tradedShares: tradedQuantity,
                            remainingShares: transaction.shares - tradedQuantity
                        };
                    }).filter(function (openTransaction) { return openTransaction.remainingShares !== 0; });
                };
                OpenTransactionListComponent = __decorate([
                    core_1.Component({
                        templateUrl: 'app/transactions/open-transaction-list.component.html',
                    }), 
                    __metadata('design:paramtypes', [transaction_service_1.TransactionService])
                ], OpenTransactionListComponent);
                return OpenTransactionListComponent;
            }());
            exports_1("OpenTransactionListComponent", OpenTransactionListComponent);
        }
    }
});
//# sourceMappingURL=open-transaction-list.component.js.map