import { Component, OnInit } from '@angular/core';
import { CursusService } from 'src/app/core/services/cursus/cursus.service';

@Component({
  selector: 'app-cursus',
  templateUrl: './cursus.component.html',
  styleUrls: ['./cursus.component.css']
})
export class CursusComponent implements OnInit {


  constructor(
    private cursusService: CursusService
  ) { }

  ngOnInit() {
  }

  loadCursussen() {

  }

}
