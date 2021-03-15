import { NonNullAssert } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

import { Task,Employee } from '../classes';
import { TaskService } from '../task.service';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  taskForm: FormGroup = new FormGroup({});
  searchTearm:string = "";
  tasks: Task[] = [];
  softwreengineers: Employee[] = [];
  closeResult = '';
  constructor(private taskService: TaskService,private employeeService: EmployeeService ,private fb: FormBuilder,private modalService: NgbModal) { }

  ngOnInit() {

    this.getTasks();
    this.getSoftwareengineers(null,null);

    this.taskForm = this.fb.group({
      id: 0,
      name: ' ',
      type: ' ',
      description: ' ',
      softwareengineerid: 0

    });
  }

  open(content:any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  getTasks(): void {
    
    this.taskService.getTasksWithSearch(this.searchTearm)
      .subscribe(tasks => this.tasks = tasks);
  }
  getSoftwareengineers(search: string | null, roleid: string | null): void {
    this.employeeService.getEmployeesWithSearch(search, "0")
      .subscribe(employees => this.softwreengineers = employees);
  }
  add(): void {

    console.log(this.taskForm.value);
     this.taskService.addTask(this.taskForm!.value)
       .subscribe(t => {
         this.tasks.push(t);
         this.getTasks();

       });
       
  }

  delete(task: Task): void {
    this.tasks = this.tasks.filter(h => h !== task);
    this.taskService.deleteTask(task.id).subscribe();
  }
  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.taskForm.reset();
    this.getTasks();

  }

    
  searchMethod(searchValue: string): void { 
{
   
  this.getTasks();
}    

}}