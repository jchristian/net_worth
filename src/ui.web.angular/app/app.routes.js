System.register(['@angular/router'], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var router_1;
    var routes, APP_ROUTER_PROVIDERS;
    return {
        setters:[
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            exports_1("routes", routes = [
                { path: '/', component: DashboardComponent },
                { path: '/dashboard', component: DashboardComponent },
                { path: '/lots', component: LotListComponent },
                { path: '/trades', component: TradeListComponent },
                { path: '/transactions', component: TransactionListComponent },
                { path: '/transactions/open', component: OpenTransactionListComponent },
                { path: '/transactions/import', component: TransactionImportComponent },
                { path: '/securities', component: SecurityListComponent },
                { path: '/security/:securityId/securityDescriptions', component: SecurityDescriptionComponent }
            ]);
            exports_1("APP_ROUTER_PROVIDERS", APP_ROUTER_PROVIDERS = [
                router_1.provideRouter(routes)
            ]);
        }
    }
});
//# sourceMappingURL=app.routes.js.map