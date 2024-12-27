import { Routes } from '@angular/router';
import { HomePageComponent } from './components/home-page/home-page.component';
import { MemberListComponent } from './components/member/member-list/member-list.component';
import { MemberDetailComponent } from './components/member/member-detail/member-detail.component';
import { ListsComponent } from './components/lists/lists.component';
import { MessagesComponent } from './components/messages/messages.component';
import { authorGuard } from './_guards/author.guard';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ServerErrorComponent } from './components/errors/server-error/server-error.component';
import { ErrtestComponent } from './components/errors/errtest/errtest.component';
import { UpdateFormComponent } from './components/update-form/update-form.component';

export const routes: Routes = [
    {path: '', component:HomePageComponent},
    {
        path:'',
        runGuardsAndResolvers: 'always',
        // canActivate: [authorGuard],
        children: [
            {path: 'member', component:MemberListComponent},
            // {path: 'member/:id', component:MemberDetailComponent},
            {path: 'member/update', component:UpdateFormComponent},
            {path: 'lists', component:ListsComponent},
            {path: 'Messages', component:MessagesComponent},
        ]
    },
    {path: 'test-error', component:ErrtestComponent},
    {path: 'not-found', component:NotFoundComponent},
    {path: 'server-error', component:ServerErrorComponent},
    {path: '', component:HomePageComponent, pathMatch:'full'},
];
