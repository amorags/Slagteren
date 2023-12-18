import { Component, OnInit } from '@angular/core';
import {State} from "../../state";

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.scss'],
})
export class CheckOutComponent  implements OnInit {

  constructor(public state: State) { }

  ngOnInit() {}

}
