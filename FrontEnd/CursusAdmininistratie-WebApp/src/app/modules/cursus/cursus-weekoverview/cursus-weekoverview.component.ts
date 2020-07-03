import { Component, OnInit } from '@angular/core';
import { CursusInstantie } from 'src/app/shared/models/cursusInstantie';
import { CursusInstantieService } from 'src/app/core/services/cursusInstantie/cursusInstantie.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CursusService } from 'src/app/core/services/cursus/cursus.service';
import { AlertService } from 'src/app/core/services/alert/alert.service';
import { WeeknumberPipe } from 'src/app/shared/pipes/weeknumber.pipe';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
@Component({
  selector: 'app-cursus-weekoverview',
  templateUrl: './cursus-weekoverview.component.html',
  styleUrls: ['./cursus-weekoverview.component.css']
})
export class CursusWeekoverviewComponent implements OnInit {

  displayedColumns: string[] = ['nav', 'startDatum', 'duur', 'titel', 'cursisten'];
  cursussen: CursusInstantie[];
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

  fb = new FormBuilder();

  constructor(
    private cursusInstantieService: CursusInstantieService,
    private cursusService: CursusService,
    private alerService: AlertService,
    private router: Router,
    actRoute: ActivatedRoute,
    private weeknumber: WeeknumberPipe) {
      this.routeYear = actRoute.snapshot.params.year !== undefined ? actRoute.snapshot.params.year : 0;
      this.routeWeek = actRoute.snapshot.params.week !== undefined ? actRoute.snapshot.params.week : 0;
     }

  ngOnInit() {

    if(this.selectedYear !== 0){
      this.selectedYear = this.routeYear;
    }else if(this.routeYear !== 0 && this.routeWeek !== 0) {
      const isValid = this.checkWeeknumber(this.routeYear, this.routeWeek);

      if(isValid){
        this.selectedWeek = this.routeWeek;
        this.selectedYear = this.routeYear;
      }else{
        this.selectedWeek = this.weeknumber.transform(this.selectedDate);
        this.selectedYear = this.selectedDate.getFullYear();
        this.alerService.errorMessage('Ingevulde week is ongeldig');
      }
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

    console.log(`Week ${this.selectedWeek} is ${this.checkWeeknumber(this.selectedYear, this.selectedWeek)}`);
    if(this.checkWeeknumber(this.selectedYear, this.selectedWeek + 1)) {

      this.selectedWeek ++;
    } else if (this.checkWeeknumber(this.selectedYear, this.selectedWeek)){

      this.selectedWeek = 1;
      this.selectedYear ++;
    }
    else{
      this.selectedWeek ++;
    }
    this.router.navigate([`cursus/weekoverzicht/${this.selectedYear}/${this.selectedWeek}`, {}]);
  }

  previousWeekToggled() {


    if  (this.selectedWeek === 1){
      this.selectedWeek = 52;
      this.selectedYear --;
    }
    else{
     this.selectedWeek --;
    }

    this.router.navigate([`cursus/weekoverzicht/${this.selectedYear}/${this.selectedWeek}`, {}]);
  }

  checkWeeknumber(year: number, week: number) {
    let lastDay = new Date(`12/31/${year}`);
    const lastWeek = this.weeknumber.transform(lastDay);
    if(week > lastWeek){
      return false;
    }else{
      return true;
    }
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
