import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Task } from './classes';


@Injectable({ providedIn: 'root' })
export class TaskService {

  private tasksUrl = 'http://localhost:44300/api/task';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

  /** GET tasks from the server */
  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.tasksUrl}`)
      .pipe(
         
        catchError(this.handleError<Task[]>('getTasks', []))
      );
  }

  /** GET tasks from the server */
  getTasksWithSearch(search:string |null): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.tasksUrl}?search=${search}`)
      .pipe(
         
        catchError(this.handleError<Task[]>('getTasks', []))
      );
  }

  /** GET Task by id. Will 404 if id not found */
  getTask(id: number): Observable<Task> {
    const url = `${this.tasksUrl}/${id}`;
    return this.http.get<Task>(url).pipe(
       
      catchError(this.handleError<Task>(`getTask id=${id}`))
    );
  }
 
  //////// Save methods //////////

  /** POST: add a new task to the server */
  addTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.tasksUrl, task, this.httpOptions).pipe(
       
      catchError(this.handleError<Task>('addTask'))
    );
  }

  
  /** PUT: update the task on the server */
  updateTask(task: Task): Observable<any> {
    return this.http.put(`${this.tasksUrl}/${task.id}`, task, this.httpOptions).pipe(
       
      catchError(this.handleError<any>('updateTask'))
    );
  }


  /** DELETE: delete the task from the server */
  deleteTask(taskId: number): Observable<Task> {
    const url = `${this.tasksUrl}/${taskId}`;

    return this.http.delete<Task>(url, this.httpOptions).pipe(
      catchError(this.handleError<Task>('deleteTask'))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
 
}