import { Routes } from '@angular/router';
import { HomeDefaultComponent } from '../../shared/layout/home/home-default/home-default.component';
import { CursusComponent } from '../../modules/cursus/cursus/cursus.component';


export const homeRoutes: Routes = [
    {path: '', component: HomeDefaultComponent},
    {
        path: '',
        component: HomeDefaultComponent,
        children: [
            {path: 'cursussen', component: CursusComponent },
            // {path: 'workschedule', component: WorkscheduleCreateComponent},
            // {path: 'registerCompany', component: CompanyRegisterComponent},
            // {path: 'locationRegister', component: LocationRegisterComponent},
            // {path: 'employees', component: EmployeesListComponent},

        ]
    },
];
