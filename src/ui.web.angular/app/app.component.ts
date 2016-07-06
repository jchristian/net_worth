import { Component } from '@angular/core';
import { HTTP_PROVIDERS } from '@angular/http';
import 'rxjs/Rx';   // Load all features
import { ROUTER_PROVIDERS, Routes, ROUTER_DIRECTIVES } from '@angular/router';

import { BasicHttpService } from './services/basic-http-service';

import { SecurityListComponent } from './securities/security-list.component';
import { SecurityDescriptionComponent } from './securities/descriptions/security-description.component';
import { SecurityService } from './securities/security.service';

import { TransactionListComponent } from './transactions/transaction-list.component';
import { OpenTransactionListComponent } from './transactions/open-transaction-list.component';
import { TransactionImportComponent } from './transactions/transaction-import.component';
import { TransactionService } from './transactions/transaction.service';

import { TradeListComponent } from './trades/trade-list.component';
import { TradeService } from './trades/trade.service';

import { LotListComponent } from './lots/lot-list.component';
import { LotService } from './lots/lot.service';

import { DashboardComponent } from './dashboard/dashboard.component';

@Component({
    selector: 'pm-app',
    templateUrl: 'app/app.component.html',
    directives: [ROUTER_DIRECTIVES],
    providers: [SecurityService,
                TransactionService,
                TradeService,
                LotService,
                BasicHttpService,
                HTTP_PROVIDERS,
                ROUTER_PROVIDERS]
})
@Routes([
    { path: '/', component: DashboardComponent },
    { path: '/dashboard', component: DashboardComponent },
    { path: '/lots', component: LotListComponent },
    { path: '/trades', component: TradeListComponent },
    { path: '/transactions', component: TransactionListComponent },
    { path: '/transactions/open', component: OpenTransactionListComponent },
    { path: '/transactions/import', component: TransactionImportComponent },
    { path: '/securities', component: SecurityListComponent },
    { path: '/security/:securityId/securityDescriptions', component: SecurityDescriptionComponent }
])
export class AppComponent {
    pageTitle: string = 'Net Worth';
}
