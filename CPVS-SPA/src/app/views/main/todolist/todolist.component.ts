import { TodolistService } from './../../../_core/_services/todolist.service';
import { AlertifyService } from './../../../_core/_services/alertify.service';
import { environment } from './../../../../environments/environment';
import { AccountService } from './../../../_core/_services/account.service';
import { Component, OnInit ,TemplateRef,ViewChild,ViewEncapsulation  } from '@angular/core';
import { saveAs } from 'file-saver';
import {
  EditService,
  ToolbarService,
  PageService,
  PageSettingsModel,
  ToolbarItems,
  GridComponent,
} from "@syncfusion/ej2-angular-grids";
import { NgbModal, NgbModalConfig, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { PlanService } from '../../../_core/_services/plan.service';
import { DatePipe } from '@angular/common';
import { Subscription } from 'rxjs';
import { SignalService } from '../../../_core/_services/signal.service';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
@Component({
  selector: 'app-todolist',
  templateUrl: './todolist.component.html',
  styleUrls: ['./todolist.component.css'],
  providers: [ToolbarService, EditService, PageService , DatePipe],
  encapsulation: ViewEncapsulation.None
})
export class TodolistComponent implements OnInit {
  planData: object;
  LineData: []
  CarData: []
  fieldsLine: object = { text: "line_Name", value: "id" };
  fieldsCar: object = { text: "car_name", value: "id" };
  editSettings = {
    showDeleteConfirmDialog: false,
    allowEditing: false,
    allowAdding: false,
    allowDeleting: true,
    mode: "Normal",
  };
  modelPlan: any ;
  dueDate: any ;
  locale = 'de-DE'
  public toolbarAccount : string[] ;
  public toolbarCarTotal : string[] ;
  pageSettings = { pageCount: 20, pageSizes: true, pageSize: 10 };
  @ViewChild("grid") public grid: GridComponent;
  private subscription: Subscription;
  private _hubConnection: HubConnection;
  public message: string;
  base_hub: any = environment.hubCPVS
  count: number = 0 ;
  planDataCount: any;
  ListDataID: any = [];
  constructor(
    private accountService: AccountService,
    private planService: PlanService,
    private alertify: AlertifyService,
    private datePipe: DatePipe,
    public _signalService: SignalService,
  ) {


    // this.subscription = this._mqttService.observe('myTopic').subscribe((message: IMqttMessage) => {
    //   this.message = message.payload.toString();
    //   console.log('Recieve from Server',this.message);
    // });
  }

  ngOnInit() {
    this.getAllPlan();
    this.getAllPlanCount();
    //this._signalService.startConnection();
    //this._signalService.ReceiveMessage();
    //this.startHttpRequest();
    this.dueDate = new Date();
    this.toolbarAccount = [
      "ExcelExport",
      "Search",
    ];
    this.toolbarCarTotal = [
      "Search",
    ];

    this._hubConnection = new HubConnectionBuilder()
    .withUrl(this.base_hub)
    .configureLogging(LogLevel.Information)
    .build();

    this._hubConnection
    .start()
    .then(() => console.log('Connection started!'))
    .catch(err => console.log('Error while establishing connection :('));
  }
  // private startHttpRequest = () => {
  //   this.getAllPlan();
  // }

  setLocalStore(key: string, value: any) {
    localStorage.removeItem(key);
    const result = JSON.stringify(value);
    localStorage.setItem(key, result);
  }

  start(data){
    this._hubConnection.on('Welcom', (message, line , id) => {
      this.getAllPlan();
      this.getAllPlanCount();
    });

    this.ListDataID.push(data.id);
    this.planService.start(data.id).subscribe((res: any)=>{
      this.getAllPlan();
      this.getAllPlanCount();
      this._hubConnection.invoke('Start', "Start", data.line_Name, data.id);
    })

  }

  stop(data){
    const index: number = this.ListDataID.indexOf(data.id);
    if (index !== -1) {
      this.ListDataID.splice(index, 1);
    }
    if (this.ListDataID.length === 0) {
      this._hubConnection.off('Welcom');
    }
    this.planService.stop(data.id).subscribe((res: any) =>{
      this.getAllPlan();
      this.getAllPlanCount();
      this._hubConnection.invoke('Stop', "Stop", data.line_Name, data.id);
    })
  }

  tooltips(data) {
    if (data) {
      return data;
    } else {
      return "";
    }
  }

  toolbarClick(args) {
    switch (args.item.text) {
      case "Excel Export":
        this.grid.excelExport({ hierarchyExportMode: "All" });
        break;
      default:
        break;
    }
  }

  async getAllPlan() {
    try {
      this.planService.getAll().subscribe((res: any) => {
        this.planData = res;
        // this.setLocalStore('todolist', this.planData);
      });
    } catch (error) {
      this.alertify.error(error + "");
    }
  }

  async getAllPlanCount() {
    try {
      this.planService.getAllCount().subscribe((res: any) => {
        this.planDataCount = res;
      });
    } catch (error) {
      this.alertify.error(error + "");
    }
  }

  NO(index) {
    return ((this.grid.pageSettings.currentPage - 1) * this.pageSettings.pageSize + Number(index) + 1);
  }

}
