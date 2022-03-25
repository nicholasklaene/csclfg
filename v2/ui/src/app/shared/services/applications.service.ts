import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ApplicationsService {
  constructor(private http: HttpClient) {}

  private readonly _applications = new BehaviorSubject()

}
