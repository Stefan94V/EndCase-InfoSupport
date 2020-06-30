import { Component, OnInit, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-home-header',
  templateUrl: './home-header.component.html',
  styleUrls: ['./home-header.component.css']
})
export class HomeHeaderComponent implements OnInit {
  @Output() toggleSideBarForMe: EventEmitter<any> = new EventEmitter();


  constructor() { }

  ngOnInit() {
  }

  toggleSideBar() {
    this.toggleSideBarForMe.emit();
  }

}
