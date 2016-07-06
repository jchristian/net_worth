import {Pipe, PipeTransform} from '@angular/core';

@Pipe({name: 'propertyNames'})
export class PropertyNamesPipe implements PipeTransform {
    transform(value: any, args?: any[]): any[] {
        return Object.keys(value);
    }
}

@Pipe({name: 'propertyValues'})
export class PropertyValuesPipe implements PipeTransform {
    transform(value: any, args?: any[]): any[] {
        return Object.keys(value).map(key => value[key]);
    }
}

@Pipe({name: 'camelToTitle'})
export class CamelToTitlePipe implements PipeTransform {
    transform(value: string[], args?: any[]): any[] {
        return value.map(title => {
            let result = title.replace( /([A-Z])/g, ' $1' );
            return result.charAt(0).toUpperCase() + result.slice(1);
        });
    }
}
