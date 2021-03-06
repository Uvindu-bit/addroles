import { Cars } from '../models/cars';
import { Component, Inject, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { objectify } from 'tslint/lib/utils';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  private http: HttpClient;
  private baseUrl: string;
  public cars: Cars[];

  constructor(http: HttpClient, @Inject('BASE_URL',) baseUrl: string, private router: Router) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    let self = this;
    this.http.get<object>(this.baseUrl + 'api/admin/getcurrentuser/upathirana112@gmail.com')
      .subscribe(
        function (data) {
          console.log(data);
        }
      )
  }
}
