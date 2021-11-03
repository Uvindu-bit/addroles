import { Cars } from '../models/cars';
import { Component, Inject, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {

  private http: HttpClient;
  private baseUrl: string;
  public cars: Cars[];

  constructor(http: HttpClient, @Inject('BASE_URL',) baseUrl: string, private router: Router) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    let self = this;
    this.cars = []
    this.http.get<Cars[]>(this.baseUrl + 'api/cars')
      .subscribe(
        function (data) {
          self.cars = data;
          console.log(self.cars);
        }
      )
  }
}
