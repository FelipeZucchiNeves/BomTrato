import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TextMaskModule } from "angular2-text-mask";

import { NgBrazil } from "ng-brazil";
import { NgxSpinnerModule } from "ngx-spinner";
import { OfficeService } from "../shared/services/office.service";
import { SharedModule } from "../shared/shared.module";

import { AddComponent } from "./add/add.component";
import { DetailsComponent } from "./details/details.component";
import { EditComponent } from "./edit/edit.component";
import { ListComponent } from "./list/list.component";
import { ProcessAppComponent } from "./process.app.component";
import { ProcessRoutingModule } from "./process.route";
import { ProcessGuard } from "./services/process.guard";
import { ProcessResolver } from "./services/process.resolve";
import { ProcessService } from "../shared/services/process.service";

@NgModule({
    declarations: [
      ProcessAppComponent,
      AddComponent,
      ListComponent,
      EditComponent,
      DetailsComponent
    ],
    imports: [
      CommonModule,
      ProcessRoutingModule,
      FormsModule,
      ReactiveFormsModule,
      NgxSpinnerModule,
      TextMaskModule,
      NgBrazil,
      NgbModule,
      HttpClientModule,
      SharedModule
    ],
    providers: [
      ProcessResolver,
      ProcessService,
      ProcessGuard
    ]
  })
  export class ProcessModule { }
  