import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-default',
  templateUrl: './home-default.component.html',
  styleUrls: ['./home-default.component.scss']
})
export class HomeDefaultComponent implements OnInit {

  sideBarOpen = true;

  constructor() { }

  ngOnInit() {
  }

  sideBarToggler() {
    this.sideBarOpen = !this.sideBarOpen;
  }

}
