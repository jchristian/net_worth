import { Component, OnInit } from '@angular/core';

import { Lot } from './lot';
import { LotService } from './lot.service';

@Component({
    templateUrl: 'app/lots/lot-list.component.html'
})
export class LotListComponent implements OnInit {
    lots: Lot[] = [];

    constructor(private service: LotService) { }

    ngOnInit() {
        this.service.getLots().subscribe((lots: Lot[]) => this.lots = lots);
    }
}
