import { AfterViewInit, Component, OnDestroy, Inject } from '@angular/core';
import { EasyDataViewDispatcher } from '@easydata/crud';

@Component({
    selector: 'app-easydata',
    template: '<div id="EasyDataContainer"></div>'
})
export class EasyDataComponent implements AfterViewInit, OnDestroy {

    private viewDispatcher: EasyDataViewDispatcher | undefined;

    // constructor(@Inject('BASE_PATH') private baseUrl: string) {
        
    //  }

    private baseUrl:String;
    constructor(){
        this.baseUrl = "https://localhost:7154/";
    }
    ngAfterViewInit() {
        this.viewDispatcher = new EasyDataViewDispatcher({
            endpoint: `${this.baseUrl}api/easydata`
        });
        this.viewDispatcher.run();
    }

    ngOnDestroy() {
        this.viewDispatcher?.detach();
    }
}
