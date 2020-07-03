import { Component, OnInit } from '@angular/core';
import { CursusInstantie } from 'src/app/shared/models/cursusInstantie';
import { CursusInstantieService } from 'src/app/core/services/cursusInstantie/cursusInstantie.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CursusService } from 'src/app/core/services/cursus/cursus.service';
import { AlertService } from 'src/app/core/services/alert/alert.service';
import { WeeknumberPipe } from 'src/app/shared/pipes/weeknumber.pipe';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';


@Component({
  selector: 'app-cursus-list',
  templateUrl: './cursus-list.component.html',
  styleUrls: ['./cursus-list.component.css']
})
export class CursusListComponent implements OnInit {
  displayedColumns: string[] = ['nav', 'startDatum', 'duur', 'titel', 'cursisten'];
  cursussen: CursusInstantie[];
  component = this;
  files: File[];


  cursussenForm: FormGroup;

  isLoading = true;
  openFileUploader = false;
  showConcept = false;
  dateFilterToggle = false;
  formIsValid = false;

  selectedStartDate = new Date();
  selectedEndDate = new Date();

  fb = new FormBuilder();

  constructor(
    private cursusInstantieService: CursusInstantieService,
    private cursusService: CursusService,
    private alerService: AlertService,
    private weeknumber: WeeknumberPipe) { }

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

  changeDateToggled(type: string, event: MatDatepickerInputEvent<Date>) {

    if(type === 'start'){
        this.selectedStartDate = new Date(event.value);
    }else if(type === 'end') {
      this.selectedEndDate = new Date(event.value);
    }
  }

  toggleDateFilter() {
    this.dateFilterToggle = !this.dateFilterToggle;
  }

  setFiles(files: File[]) {
    this.files = files;
    this.formIsValid = true;
  }

  upload() {
    this.openFileUploader = false;
    this.isLoading = true;
    this.formIsValid = false;

    if (this.dateFilterToggle){
      console.log('upload met dates');
      this.cursusService.uploadCursusInRange(this.files, this.selectedStartDate, this.selectedEndDate).subscribe((cs: {
        duplicates: CursusInstantie[],
        uploaded: CursusInstantie[],
        message: string
      }) => {
        this.cursussen = this.cursussen.concat(cs.uploaded);
        if(cs.message != ''){
        this.alerService.errorMessage(`${cs.message} Er zijn geen cursusinstanties toegevoegd.`);
        }else{
          const unique_cursussen = [...new Set(cs.uploaded.map(item => item.code))];

          this.alerService.successMessage(`Er zijn ${unique_cursussen.length} cursussen, en ${cs.uploaded.length} cursusinstanties toegevoegd. Er zijn ${cs.duplicates.length} duplicaten gevonden`);
        }
        this.isLoading = false;
      }, () => {
        this.files = [];
      });
    }else{
      console.log('upload zonder dates');

      this.cursusService.uploadCursus(this.files).subscribe((cs: {
        duplicates: CursusInstantie[],
        uploaded: CursusInstantie[],
        message: string
      }) => {
        this.cursussen = this.cursussen.concat(cs.uploaded);
        if(cs.message != ''){
        this.alerService.errorMessage(`${cs.message} Er zijn geen cursusinstanties toegevoegd.`);
        }else{
          const unique_cursussen = [...new Set(cs.uploaded.map(item => item.code))];

          this.alerService.successMessage(`Er zijn ${unique_cursussen.length} cursussen, en ${cs.uploaded.length} cursusinstanties toegevoegd. Er zijn ${cs.duplicates.length} duplicaten gevonden`);
        }
        this.isLoading = false;
      }, () => {
        this.files = [];
      });
    }
  }


}
