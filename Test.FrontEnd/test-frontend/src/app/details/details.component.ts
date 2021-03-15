import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { switchMap } from 'rxjs/operators';
import { TaskDetail } from '../classes';
import { DetailService } from '../detail.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  taskDetailsForm: FormGroup = new FormGroup({});
  closeResult = '';
  id:any;
  taskDetails : TaskDetail[] = [];
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private service: DetailService,
    private fb: FormBuilder,private modalService: NgbModal
  ) {}
  ngOnInit() {
     this.id = this.route.snapshot.paramMap.get('id');

    this.getTaskDetails(null, this.id);
    
    this.taskDetailsForm = this.fb.group({
      id: 0,
      description: '',
      startDate: '',
      endDate: '',
      taskId: this.id

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

  add(): void {

    console.log(this.taskDetailsForm.value);
     this.service.addTaskDetail(this.taskDetailsForm.value)
       .subscribe(t => {
         this.taskDetails.push(t);
         this.getTaskDetails(null, this.id);

       });
       
  }



  getTaskDetails(search: string | null, taskid: string | null): void {
    this.service.getTaskDetailWithSearch(search,taskid)
      .subscribe(tds => this.taskDetails = tds);
  }

}
