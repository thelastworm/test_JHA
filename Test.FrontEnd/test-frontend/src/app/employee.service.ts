import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Employee, Task } from './classes';


@Injectable({ providedIn: 'root' })
export class EmployeeService {

  private tasksUrl = 'http://localhost:44300/api/employee';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  /** GET employees from the server */
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.tasksUrl}`)
      .pipe(
         
        catchError(this.handleError<Employee[]>('getEmployees', []))
      );
  }

  /** GET employees from the server */
  getEmployeesWithSearch(search:string |null,roleId:string |null): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.tasksUrl}?search=${search}&roleId=${roleId}`)
      .pipe(
         
        catchError(this.handleError<Employee[]>('getTasks', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
