import { Component, OnInit } from '@angular/core';
import { CursusInstantie } from 'src/app/shared/models/cursusInstantie';
import { CursusInstantieService } from 'src/app/core/services/cursusInstantie/cursusInstantie.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CursusService } from 'src/app/core/services/cursus/cursus.service';
import { AlertService } from 'src/app/core/services/alert/alert.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-cursus-list',
  templateUrl: './cursus-list.component.html',
  styleUrls: ['./cursus-list.component.css']
})
export class CursusListComponent implements OnInit {
  displayedColumns: string[] = ['startDatum', 'duur', 'titel', 'cursisten'];
  conceptColumns: string[] = ['startDatum', 'duur', 'titel'];
  cursussen: CursusInstantie[];
  conceptCursussen: CursusInstantie[] = [];
  component = this;

  cursussenForm: FormGroup;

  isLoading = true;
  openFileUploader = false;
  showConcept = false;

  constructor(
    private cursusInstantieService: CursusInstantieService,
    private cursusService: CursusService,
    private alerService: AlertService,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.loadData();
    this.initializeForm();
  }

  loadData() {
    this.isLoading = true;
    this.cursusInstantieService.getCursusInstanties().subscribe(cs => {
      this.cursussen = cs;
      this.isLoading = false;
    }, error => {
      console.log(error);
      this.isLoading = true;
    });
  }

  initializeForm() {
    this.cursussenForm = this.fb.group({
      file: new FormControl(undefined, [Validators.required, this.requiredFileType('txt')])
    });
  }

  fileUploaderToggle() {
    this.openFileUploader = !this.openFileUploader;
  }

   requiredFileType( type: string ) {
    return function (control: FormControl) {
      const file = control.value;
      if ( file ) {
        const extension = file.name.split('.')[1].toLowerCase();
        if ( type.toLowerCase() !== extension.toLowerCase() ) {
          return {
            requiredFileType: true
          };
        }

        return null;
      }
      return null;
    };
  }

  loadConcept(cursussen: CursusInstantie[]) {
    this.conceptCursussen = cursussen;
    this.showConcept = true;
  }

  uploadConcept() {

  }


  upload(files: File[]) {
    this.openFileUploader = false;
    this.isLoading = true;
    this.cursusService.uploadCursus(files).subscribe((cs: {
      duplicates: CursusInstantie[],
      uploaded: CursusInstantie[],
      message: string
    }) => {
      this.cursussen = this.cursussen.concat(cs.uploaded);
      this.alerService.successMessage(`${cs.uploaded.length} toegevoegd, ${cs.duplicates.length} duplicaten gevonden`);
      if(cs.message != ''){
      this.alerService.errorMessage(`${cs.message}`);
      }
      this.isLoading = false;
    });

  }


}
