import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cursist } from '../../../shared/models/cursist';

@Injectable({
  providedIn: 'root'
})
export class CursistService {
  baseUrl = environment.apiUrl;
  controllerName = '/cursisten';

  constructor(private http: HttpClient) { }

  createCursist(naam: string, achternaam: string, cursusInstantieId: string): Observable<Cursist> {
    return this.http.post<Cursist>(this.baseUrl + this.controllerName, {
      naam: naam,
      achternaam: achternaam,
      cursusInstantieId: cursusInstantieId
    });
  }

  getCursist(cursistId: string): Observable<Cursist> {
    return this.http.get<Cursist>(this.baseUrl + this.controllerName + '/' + `${cursistId}`);
  }

  getCursisten(): Observable<Cursist[]> {
    return this.http.get<Cursist[]>(this.baseUrl + this.controllerName);
  }

  updateCursist(cursist: Cursist): Observable<Cursist>{
    return this.http.put<Cursist>(this.baseUrl +  this.controllerName, cursist);
  }

  removeCursist(id: string) {
    return this.http.delete(this.baseUrl + this.controllerName + '/' + id);
  }

}
