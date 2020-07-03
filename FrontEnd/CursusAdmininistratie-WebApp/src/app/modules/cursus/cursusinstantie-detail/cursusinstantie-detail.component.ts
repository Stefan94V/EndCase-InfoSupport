import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CursusInstantieService } from 'src/app/core/services/cursusInstantie/cursusInstantie.service';
import { AlertService } from 'src/app/core/services/alert/alert.service';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { CursusInstantie } from 'src/app/shared/models/cursusInstantie';
import { Cursist } from '../../../shared/models/cursist';
import { CursistService } from 'src/app/core/services/cursist/cursist.service';

@Component({
  selector: 'app-cursusinstantie-detail',
  templateUrl: './cursusinstantie-detail.component.html',
  styleUrls: ['./cursusinstantie-detail.component.css']
})
export class CursusinstantieDetailComponent implements OnInit {
  id: string;
  cursusInstantie: CursusInstantie;
  cursistForm : FormGroup;
  cursusInstantieSpinner = true;
  addCursistToggled = false;

  fb = new FormBuilder();

  constructor(
    private cursusInstantieService: CursusInstantieService,
    private alerService: AlertService,
    actRoute: ActivatedRoute,
    private cursistService: CursistService,
    private router: Router,
    ) {
      this.id = actRoute.snapshot.params.id !== undefined ? actRoute.snapshot.params.id : '0';
     }

  ngOnInit() {
    if(this.id == '0'){
      this.alerService.errorMessage('Cursusinstantie niet gevonden');
      this.router.navigateByUrl('/cursus');
    }else{
      this.loadCursusInstantie();
    }
  }

  setupForm() {
    this.cursistForm = this.fb.group({
      naam: new FormControl(undefined, Validators.required),
      achternaam: new FormControl(undefined, Validators.required),
      cursistInstantie: new FormControl(this.id, Validators.required)
    });
  }

  addCursistToggle() {
    this.addCursistToggled = !this.addCursistToggled;
  }

  loadCursusInstantie() {
    this.cursusInstantieService.getCursusInstantie(this.id).subscribe(ci => {
      this.cursusInstantie = ci;
      this.setupForm();
      this.cursusInstantieSpinner = false;

    }, error => {
      this.alerService.errorMessage('Cursusinstantie niet gevonden');
      this.router.navigateByUrl('/cursus');
      this.cursusInstantieSpinner = false;

    })
  }

  save() {
    this.cursistService.createCursist(this.cursistForm.value.naam, this.cursistForm.value.achternaam, this.id).subscribe(c => {
      this.alerService.successMessage('Cursist added ');
      this.addCursistToggle();
    }, error => {
      this.alerService.errorMessage('Cursist niet toegevoegd');
    });
  }

}
