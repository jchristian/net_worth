import { Component, Input } from '@angular/core';
import { PropertyNamesPipe, PropertyValuesPipe, CamelToTitlePipe } from '../shared/object-properties.pipe';

@Component({
    selector: 'basic-table',
    templateUrl: 'app/shared/basic-table.component.html',
    pipes: [PropertyNamesPipe, PropertyValuesPipe, CamelToTitlePipe]
})
export class BasicTableComponent {
    @Input() items: any[];
}
