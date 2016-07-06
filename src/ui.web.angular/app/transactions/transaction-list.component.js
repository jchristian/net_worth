System.register(['@angular/core', '@angular/router', './transaction.service', './transaction-import.component', '../shared/basic-table.component'], function(exports_1, context_1) {
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
    var core_1, router_1, transaction_service_1, transaction_import_component_1, basic_table_component_1;
    var TransactionListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (transaction_service_1_1) {
                transaction_service_1 = transaction_service_1_1;
            },
            function (transaction_import_component_1_1) {
                transaction_import_component_1 = transaction_import_component_1_1;
            },
            function (basic_table_component_1_1) {
                basic_table_component_1 = basic_table_component_1_1;
            }],
        execute: function() {
            TransactionListComponent = (function () {
                function TransactionListComponent(service) {
                    this.service = service;
                    this.transactions = [];
                }
                TransactionListComponent.prototype.ngOnInit = function () {
                    this.getTransactions();
                };
                TransactionListComponent.prototype.onImportSucceeded = function () {
                    this.getTransactions();
                };
                TransactionListComponent.prototype.getTransactions = function () {
                    var _this = this;
                    this.service.getTransactions()
                        .subscribe(function (transactions) { return _this.transactions = transactions; }, function (error) { return _this.errorMessage = error; });
                };
                TransactionListComponent = __decorate([
                    core_1.Component({
                        templateUrl: 'app/transactions/transaction-list.component.html',
                        directives: [router_1.ROUTER_DIRECTIVES, transaction_import_component_1.TransactionImportComponent, basic_table_component_1.BasicTableComponent],
                    }), 
                    __metadata('design:paramtypes', [transaction_service_1.TransactionService])
                ], TransactionListComponent);
                return TransactionListComponent;
            }());
            exports_1("TransactionListComponent", TransactionListComponent);
        }
    }
});
//# sourceMappingURL=transaction-list.component.js.map