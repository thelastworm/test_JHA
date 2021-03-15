 

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { TaskDetail } from './classes';


@Injectable({ providedIn: 'root' })
export class DetailService {

  private taskDetailUrl = 'http://localhost:44300/api/taskdetails';  // URL to web api

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(
    private http: HttpClient) { }

 
  getTaskDetailWithSearch(search:string |null,taskId:string |null): Observable<TaskDetail[]> {
    
    return this.http.get<TaskDetail[]>(`${this.taskDetailUrl}?search=${search}&taskId=${taskId}`)
      .pipe(
        catchError(this.handleError<TaskDetail[]>('getTaskDetails', []))
      );

  }


  /** POST: add a new taskDetail to the server */
  addTaskDetail(taskDetail: TaskDetail): Observable<TaskDetail> {
    return this.http.post<TaskDetail>(this.taskDetailUrl, taskDetail, this.httpOptions).pipe(
       
      catchError(this.handleError<TaskDetail>('addTaskDetail'))
    );
  }

  
  // /** PUT: update the task on the server */
  // updateTask(task: Task): Observable<any> {
  //   return this.http.put(`${this.tasksUrl}/${task.id}`, task, this.httpOptions).pipe(
       
  //     catchError(this.handleError<any>('updateTask'))
  //   );
  // }


  // /** DELETE: delete the task from the server */
  // deleteTask(taskId: number): Observable<Task> {
  //   const url = `${this.tasksUrl}/${taskId}`;

  //   return this.http.delete<Task>(url, this.httpOptions).pipe(
  //     catchError(this.handleError<Task>('deleteTask'))
  //   );
  // }

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