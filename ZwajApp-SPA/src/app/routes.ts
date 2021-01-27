import { Routes } from '@angular/router'
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MembersComponent } from './members/members.component';
import { MessagesComponent } from './messages/messages.component';
export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'members', component: MembersComponent },
    { path: 'lists', component: ListsComponent },
    { path: 'messages', component: MessagesComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];