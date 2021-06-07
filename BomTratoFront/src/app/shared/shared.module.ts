import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AproverService } from "./services/aprover.service";
import { OfficeService } from "./services/office.service";
import { ProcessService } from "./services/process.service";

@NgModule({
    imports: [
        CommonModule
    ],
    exports: [
        CommonModule
    ],
    providers: [
        OfficeService,
        AproverService,
        ProcessService
    ]
  })
  export class SharedModule { }