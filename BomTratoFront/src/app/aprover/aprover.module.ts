import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { NgxSpinnerModule } from "ngx-spinner";
import { AddComponent } from "./add/add.component";
import { AproverAppComponent } from "./aprover.app.component";
import { AproverRoutingModule } from "./aprover.route";
import { DetailsComponent } from "./details/details.component";
import { EditComponent } from "./edit/edit.component";
import { ListComponent } from "./list/list.component";
import { RemoveComponent } from "./remove/remove.component";
import { AproverResolver } from "./services/aprover.resolve";
import { AproverService } from "../shared/services/aprover.service";
import { SharedModule } from "../shared/shared.module";
import { AproverGuard } from "./services/aprover.guard";

@NgModule({
    declarations:[
        AproverAppComponent,
        AddComponent,
        ListComponent,
        RemoveComponent,
        DetailsComponent,
        EditComponent
    ],
    imports: [
        CommonModule,
        AproverRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        NgxSpinnerModule,
        NgbModule,
        HttpClientModule,
        SharedModule
    ],
    providers: [
        AproverService,
        AproverResolver,
        AproverGuard
    ]
})
export class AproverModule{ }