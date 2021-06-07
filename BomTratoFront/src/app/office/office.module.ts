import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TextMaskModule } from "angular2-text-mask";

import { NgBrazil } from "ng-brazil";
import { NgxSpinnerModule } from "ngx-spinner";

import { AddComponent } from "./add/add.component";
import { DetailsComponent } from "./details/details.component";
import { EditComponent } from "./edit/edit.component";
import { ListComponent } from "./list/list.component";
import { OfficeAppComponent } from "./office.app.component";
import { OfficeRoutingModule } from "./office.route";
import { RemoveComponent } from "./remove/remove.component";
import { OfficeResolver } from "./services/office.resolve";
import { OfficeService } from "../shared/services/office.service";
import { SharedModule } from "../shared/shared.module";
import { OfficeGuard } from "./services/office.guard";

@NgModule({
    declarations: [
      OfficeAppComponent,
      AddComponent,
      ListComponent,
      EditComponent,
      RemoveComponent,
      DetailsComponent
    ],
    imports: [
      CommonModule,
      OfficeRoutingModule,
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
      OfficeResolver,
      OfficeService,
      OfficeGuard
    ]
  })
  export class OfficeModule { }
  