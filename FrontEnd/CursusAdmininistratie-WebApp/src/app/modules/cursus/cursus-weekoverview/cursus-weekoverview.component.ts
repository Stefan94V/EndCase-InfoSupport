import { Component, OnInit } from '@angular/core';
import { CursusInstantie } from 'src/app/shared/models/cursusInstantie';
import { CursusInstantieService } from 'src/app/core/services/cursusInstantie/cursusInstantie.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CursusService } from 'src/app/core/services/cursus/cursus.service';
import { AlertService } from 'src/app/core/services/alert/alert.service';
import { WeeknumberPipe } from 'src/app/shared/pipes/weeknumber.pipe';
import { ActivatedRoute } from '@angular/router';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
@Component({
  selector: 'app-cursus-weekoverview',
  templateUrl: './cursus-weekoverview.component.html',
  styleUrls: ['./cursus-weekoverview.component.css']
})
export class CursusWeekoverviewComponent implements OnInit {

  displayedColumns: string[] = ['startDatum', 'duur', 'titel', 'cursisten'];
  conceptColumns: string[] = ['startDatum', 'duur', 'titel'];
  cursussen: CursusInstantie[];
  conceptCursussen: CursusInstantie[] = [];
  component = this;
  selectedDate = new Date();

  selectedWeek = 0;
  selectedYear = 0;

  routeYear: number;
  routeWeek: number;

  cursussenForm: FormGroup;

  isLoading = true;
  openFileUploader = false;
  showConcept = false;

  constructor(
    private cursusInstantieService: CursusInstantieService,
    private cursusService: CursusService,
    private alerService: AlertService,
    actRoute: ActivatedRoute,
    private weeknumber: WeeknumberPipe,
    private fb: FormBuilder) {
      this.routeYear = actRoute.snapshot.params.year !== undefined ? actRoute.snapshot.params.year : 0;
      this.routeWeek = actRoute.snapshot.params.week !== undefined ? actRoute.snapshot.params.week : 0;
     }

  ngOnInit() {

    if(this.selectedYear !== 0){
      this.selectedYear = this.routeYear;
    }else if(this.routeYear !== 0 && this.routeWeek !== 0) {
      this.selectedWeek = this.routeWeek;
      this.selectedYear = this.routeYear;
    }else{
      this.selectedWeek = this.weeknumber.transform(this.selectedDate);
      this.selectedYear = this.selectedDate.getFullYear();
    }

    this.loadData();
  }

  loadData() {
    this.isLoading = true;
    this.cursusInstantieService.getCursusInstantiesByWeekAndYear(this.selectedYear, this.selectedWeek).subscribe(cs => {
      this.cursussen = cs;
      this.isLoading = false;

    }, error => {
      console.log(error);
      this.isLoading = true;
    });
  }

  nextWeekToggled() {
    if  (this.selectedWeek === 52){
      this.selectedWeek = 1;
      this.selectedYear ++;
    }
    else{
      this.selectedWeek ++;
    }
    this.loadData();
  }

  previousWeekToggled() {
    if  (this.selectedWeek === 1){
      this.selectedWeek = 52;
      this.selectedYear --;
    }
    else{
     this.selectedWeek --;
    }
    this.loadData();
  }

  changeDateToggled(type: string, event: MatDatepickerInputEvent<Date>) {
    console.log(`${type}: ${event.value}`);

    const chosenDate = new Date(event.value);
    this.selectedWeek = this.weeknumber.transform(chosenDate);
    this.selectedYear = chosenDate.getFullYear();
    this.selectedDate = chosenDate;
    this.loadData();
  }

}
