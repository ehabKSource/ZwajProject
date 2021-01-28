import { Routes } from '@angular/router'
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MembersComponent } from './members/members.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'members', component: MembersComponent , canActivate: [AuthGuard] },
    { path: 'lists', component: ListsComponent , canActivate: [AuthGuard] },
    { path: 'messages', component: MessagesComponent, canActivate: [AuthGuard] },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];