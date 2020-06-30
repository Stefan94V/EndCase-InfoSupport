import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Cursus } from 'src/app/shared/models/cursus';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CursusService {
  baseUrl = environment.apiUrl;
  controllerName = '/cursussen';

  constructor(private http: HttpClient) { }

  createCursus(cursus: Cursus): Observable<Cursus> {
    return this.http.post<Cursus>(this.baseUrl + this.controllerName, cursus);
  }

  getCursus(cursusId: string): Observable<Cursus> {
    return this.http.get<Cursus>(this.baseUrl + this.controllerName + '/' + `${cursusId}`);
  }

  getCursussus(): Observable<Cursus[]> {
    return this.http.get<Cursus[]>(this.baseUrl + this.controllerName);
  }

  updateCursus(cursus: Cursus): Observable<Cursus>{
    return this.http.put<Cursus>(this.baseUrl +  this.controllerName, cursus);
  }

  removeCursus(id: string) {
    return this.http.delete(this.baseUrl + this.controllerName + '/' + id);
  }

  uploadCursus(files: File[]) {
      var formData = new FormData();
      Array.from(files).forEach(f => formData.append("files", f));
      return this.http
        .post(this.baseUrl + '/FileUpload/', formData);
  }
}
