import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddComponent } from "./add/add.component";
import { DetailsComponent } from "./details/details.component";
import { EditComponent } from "./edit/edit.component";
import { ListComponent } from "./list/list.component";
import { OfficeAppComponent } from "./office.app.component";
import { RemoveComponent } from "./remove/remove.component";
import { OfficeGuard } from "./services/office.guard";
import { OfficeResolver } from "./services/office.resolve";

const officeRouterConfig: Routes = [
    {
        path: '', component: OfficeAppComponent,
        children: [
            {path: 'listar-todos', component: ListComponent, canActivate: [OfficeGuard], canDeactivate: [OfficeGuard] },
            {path: 'adicionar-novo', component: AddComponent, canActivate: [OfficeGuard], canDeactivate: [OfficeGuard] },
            {path: 'editar/:id', component: EditComponent, canActivate: [OfficeGuard], canDeactivate: [OfficeGuard] ,
                resolve: {
                    office: OfficeResolver
                }},
            {path: 'detalhes/:id', component: DetailsComponent, canActivate: [OfficeGuard], canDeactivate: [OfficeGuard],
                resolve: {
                    office: OfficeResolver
                }},
            {path: 'excluir/:id', component: RemoveComponent, canActivate: [OfficeGuard], canDeactivate: [OfficeGuard],
                resolve: {
                    office: OfficeResolver
                }}
        ]
    }
];


@NgModule({
    imports: [
        RouterModule.forChild(officeRouterConfig)
    ],
    exports: [RouterModule]
})
export class OfficeRoutingModule { }