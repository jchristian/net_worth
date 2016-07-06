import { Component, OnInit } from '@angular/core';

import { BasicTableComponent } from '../shared/basic-table.component';
import { TradeService } from './trade.service';
import { Trade } from './trade';

@Component({
    templateUrl: 'app/trades/trade-list.component.html',
    directives: [BasicTableComponent]
})
export class TradeListComponent implements OnInit {
    trades: Trade[] = [];
    errorMessage: string;

    constructor(private service: TradeService) { }

    ngOnInit() {
        this.service.getTrades()
            .subscribe((trades: Trade[]) => this.trades = trades,
                        error => this.errorMessage = <any>error);
    }
}
