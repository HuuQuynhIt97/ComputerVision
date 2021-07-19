import { PlanService } from './../../../_core/_services/plan.service';
import { AlertifyService } from '../../../_core/_services/alertify.service';
import { environment } from '../../../../environments/environment';
import { Component, OnInit ,ViewChild,ViewEncapsulation  } from '@angular/core';
import {
  EditService,
  ToolbarService,
  PageService,
  PageSettingsModel,
  ToolbarItems,
  GridComponent,
} from "@syncfusion/ej2-angular-grids";
import { AccountService } from '../../../_core/_services/account.service';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-work-plan',
  templateUrl: './work-plan.component.html',
  styleUrls: ['./work-plan.component.css'],
  providers: [ToolbarService, EditService, PageService, DatePipe],
  encapsulation: ViewEncapsulation.None
})
export class WorkPlanComponent implements OnInit {

  planData: object;
  LineData: []
  CarData: []
  fieldsLine: object = { text: "line_Name", value: "id" };
  fieldsCar: object = { text: "car_name", value: "id" };
  editSettings = {
    showDeleteConfirmDialog: false,
    allowEditing: true,
    allowAdding: true,
    allowDeleting: true,
    mode: "Normal",
  };
  modelPlan: any ;
  Line_name: any ;
  dueDate: any ;
  locale = 'de-DE'
  public toolbarAccount : string[] ;
  pageSettings = { pageCount: 20, pageSizes: true, pageSize: 10 };
  @ViewChild("grid") public grid: GridComponent;

  constructor(
    private accountService: AccountService,
    private planService: PlanService,
    private alertify: AlertifyService,
    private datePipe: DatePipe
  ) {

  }

  ngOnInit() {
    this.getAllPlan();
    this.getAllCar();
    this.getAllLine();
    this.dueDate = new Date();
    this.toolbarAccount = [
      "Add",
      "Cancel",
      "ExcelExport",
      "Search",
    ];
  }

  onChangeLine(args) {
    this.Line_name = args.itemData.line_Name
  }

  onChangeCar(args) {
  }

  tooltips(data) {
    if (data) {
      return data;
    } else {
      return "";
    }
  }

  getAllLine() {
    this.planService.getAllLine().subscribe((res: any) => {
      this.LineData = res ;
    })
  }
  getAllCar() {
    this.planService.getAllCar().subscribe((res: any) => {
      this.CarData = res ;
    })
  }
  onChangeDueDateEdit(args) {
    if (args.isInteracted) {
      this.dueDate = this.datePipe.transform(args.value, 'yyyy-MM-dd')
    }
  }
  create() {
    this.planService.created(this.modelPlan).subscribe((res: any) => {
      if(res) {
        this.alertify.success("Plan has been created!");
        this.getAllPlan();
      }
    })
  }

  delete(data) {
  }

  actionBegin(args) {
    if (args.requestType === "save" && args.action === "add") {
      this.modelPlan = {
        Line_Name: this.Line_name,
        Car_Name: args.data.car_Name,
        DueDate: this.dueDate
      }
      this.create();
    }
    if (args.requestType === "save" && args.action === "edit") {

    }
    if (args.requestType === "delete") {
      // this.delete(args.data[0]);
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

  actionComplete(args) {
    // if (args.requestType === "edit") {
    //   (args.form.elements.namedItem("ID") as HTMLInputElement).disabled = true;
    //   (args.form.elements.namedItem(
    //     "Password"
    //   ) as HTMLInputElement).disabled = true;
    // }
    // if (args.requestType === "add") {
    //   (args.form.elements.namedItem("Status") as HTMLInputElement).disabled = true;
    // }
  }

  dataBound() {
    // document.querySelectorAll(
    //   "button[aria-label=Update] > span.e-tbar-btn-text"
    // )[0].innerHTML = "Save";
  }

  async getAllPlan() {
    try {
      this.planService.getAll().subscribe((res: any) => {
        // const users = res.map((item: any) => {
        //   return {
        //     ID: item.id,
        //     Username: item.username,
        //     Password: this.passwordFake + item.id,
        //     Email: item.email,
        //     EmployeeID: item.employeeID,
        //     Status: this.StatusTemplate(item.id),
        //     RoleID: this.RoleIDTempate(item.id),
        //     RoleName: this.RoleTempate(item.id),
        //     // BuildingName: this.buildingTempate(item.ID),
        //   };
        // });
        this.planData = res;
      });
    } catch (error) {
      this.alertify.error(error + "");
    }
  }

  NO(index) {
    return (
      (this.grid.pageSettings.currentPage - 1) * this.pageSettings.pageSize +
      Number(index) +
      1
    );
  }


}
