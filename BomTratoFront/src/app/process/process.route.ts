import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AddComponent } from "./add/add.component";
import { DetailsComponent } from "./details/details.component";
import { EditComponent } from "./edit/edit.component";
import { ListComponent } from "./list/list.component";
import { ProcessAppComponent } from "./process.app.component";
import { ProcessGuard } from "./services/process.guard";
import { ProcessResolver } from "./services/process.resolve";

const processRouterConfig: Routes = [
    {
        path: '', component: ProcessAppComponent,
        children: [
            {path: 'listar-todos', component: ListComponent,canActivate: [ProcessGuard], canDeactivate: [ProcessGuard] },
            {path: 'adicionar-novo', component: AddComponent,canActivate: [ProcessGuard], canDeactivate: [ProcessGuard]},
            {path: 'editar/:id', component: EditComponent,canActivate: [ProcessGuard], canDeactivate: [ProcessGuard],
                resolve: {
                    process: ProcessResolver
                }},
            {path: 'detalhes/:id', component: DetailsComponent,canActivate: [ProcessGuard], canDeactivate: [ProcessGuard],
                resolve: {
                    process: ProcessResolver
                }}
        ]
    }
];


@NgModule({
    imports: [
        RouterModule.forChild(processRouterConfig)
    ],
    exports: [RouterModule]
})
export class ProcessRoutingModule { }