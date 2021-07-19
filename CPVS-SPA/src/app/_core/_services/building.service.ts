import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class BuildingService {

  baseUrl = environment.api;
  messageSource = new BehaviorSubject<number>(0);
  currentMessage = this.messageSource.asObservable();
  // method này để change source message
  changeMessage(message) {
    this.messageSource.next(message);
  }
  constructor(private http: HttpClient) { }
  delete(id) { return this.http.delete(`${this.baseUrl}Building/Delete/${id}`); }
  rename(edit) { return this.http.put(`${this.baseUrl}Building/Update`, edit); }

  getBuildingsAsTreeView() {
    return this.http.get(`${this.baseUrl}Building/GetAllAsTreeView`);
  }
  createMainBuilding(Building) { return this.http.post(`${this.baseUrl}Building/CreateMainBuilding`, Building); }
  createSubBuilding(Building) { return this.http.post(`${this.baseUrl}Building/CreateSubBuilding`, Building); }
}
