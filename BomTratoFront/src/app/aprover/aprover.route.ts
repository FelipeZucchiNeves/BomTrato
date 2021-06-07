import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AddComponent } from "./add/add.component";
import { AproverAppComponent } from "./aprover.app.component";
import { DetailsComponent } from "./details/details.component";
import { EditComponent } from "./edit/edit.component";
import { ListComponent } from "./list/list.component";
import { RemoveComponent } from "./remove/remove.component";
import { AproverGuard } from "./services/aprover.guard";
import { AproverResolver } from "./services/aprover.resolve";

const aproverRouterConfig: Routes = [
    {
        path: '', component: AproverAppComponent,
        children: [
            {path: 'listar-todos', component: ListComponent, canActivate: [AproverGuard], canDeactivate: [AproverGuard]},
            {path: 'adicionar-novo', component: AddComponent, canActivate: [AproverGuard], canDeactivate: [AproverGuard]},
            {path: 'editar/:id', component: EditComponent, canActivate: [AproverGuard], canDeactivate: [AproverGuard],
                resolve: {
                    aprover: AproverResolver
                }},
            {path: 'detalhes/:id', component: DetailsComponent, canActivate: [AproverGuard], canDeactivate: [AproverGuard],
                resolve: {
                    aprover: AproverResolver
            }},
            {path: 'excluir/:id', component: RemoveComponent, canActivate: [AproverGuard], canDeactivate: [AproverGuard],
                resolve: {
                    aprover: AproverResolver
            }}
        ]
    }
];


@NgModule({
    imports: [
        RouterModule.forChild(aproverRouterConfig)
    ],
    exports: [
        RouterModule
    ]
})
export class AproverRoutingModule{ }