import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { CursusInstantie } from 'src/app/shared/models/cursusInstantie';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CursusInstantieService {
  baseUrl = environment.apiUrl;
  controllerName = '/cursusinstanties';

  constructor(private http: HttpClient) { }


  getCursusInstantie(cursusId: string): Observable<CursusInstantie> {
    return this.http.get<CursusInstantie>(this.baseUrl + this.controllerName + '/' + `${cursusId}`);
  }

  getCursusInstanties(): Observable<CursusInstantie[]> {
    return this.http.get<CursusInstantie[]>(this.baseUrl + this.controllerName);
  }

  getCursusInstantiesByWeekAndYear(year: number, week: number): Observable<CursusInstantie[]> {
    return this.http.get<CursusInstantie[]>(this.baseUrl + this.controllerName + `/?year=${year}&week=${week}`);
  }

}
