
import { Routes } from '@angular/router';
import { HomeDefaultComponent } from '../../shared/layout/home/home-default/home-default.component';
import { CursusComponent } from '../../modules/cursus/cursus/cursus.component';
import { CursusWeekoverviewComponent } from 'src/app/modules/cursus/cursus-weekoverview/cursus-weekoverview.component';
import { CursusListComponent } from 'src/app/modules/cursus/cursus-list/cursus-list.component';
import { CursusinstantieDetailComponent } from 'src/app/modules/cursus/cursusinstantie-detail/cursusinstantie-detail.component';


export const homeRoutes: Routes = [
    {path: '', component: HomeDefaultComponent},
    {
        path: '',
        component: HomeDefaultComponent,
        children: [
            {
              path: 'cursus',
              component: CursusComponent,
              children: [
              {path: 'weekoverzicht', component: CursusWeekoverviewComponent },
              {path: 'weekoverzicht/:year', component: CursusWeekoverviewComponent },
              {path: 'weekoverzicht/:year/:week', component: CursusWeekoverviewComponent },
              {path: 'cursussen', component: CursusListComponent },
              {path: 'instantie/:id', component: CursusinstantieDetailComponent }
            ]},
        ]
    },
];
