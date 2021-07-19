import { OcUserComponent } from './../oc-user/oc-user.component';
import { BuildingService } from './../../../../_core/_services/building.service';
import { AlertifyService } from './../../../../_core/_services/alertify.service';
import { Component, OnInit, TemplateRef, ViewChild } from "@angular/core";
import { NgbActiveModal, NgbModal, NgbModalRef } from "@ng-bootstrap/ng-bootstrap";
import { TreeGridComponent } from "@syncfusion/ej2-angular-treegrid";
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AccountComponent } from '../account/account.component';
@Component({
  selector: 'app-oc',
  templateUrl: './oc.component.html',
  styleUrls: ['./oc.component.css']
})
export class OcComponent implements OnInit {
  @ViewChild('content', { static: true }) content: TemplateRef<any>;
  toolbar: object;
  data: any;
  editing: any;
  contextMenuItems: any;
  pageSettings: any;
  editparams: { params: { format: string } };
  @ViewChild('childModal', { static: false }) childModal: ModalDirective;
  @ViewChild("treegrid")
  public treeGridObj: TreeGridComponent;
  @ViewChild("buildingModal")
  buildingModal: any;
  building: { id: 0; name: ""; level: 0; parentID: 0 };
  edit: { id: 0; name: ""; level: 0; parentID: 0 };
  title: string;
  modalReference: NgbModalRef
  constructor(
    private buildingService: BuildingService,
    private modalServices: NgbModal,
    private alertify: AlertifyService,

  ) {}

  ngOnInit() {
    this.editing = { allowDeleting: true, allowEditing: true, mode: "Row" };
    this.toolbar = ["Delete", "Search", "Update", "Cancel"];
    this.optionTreeGrid();
    this.onService();
  }

  validation() {
    if (this.building.name === '') {
      this.alertify.warning('Please enter building name!');
      return false;
    } else {
      return true;
    }
  }

  createBuilding() {
    if (this.validation()) {
      if (this.building.parentID > 0) {
        this.buildingService.createSubBuilding(this.building).subscribe(res => {
          this.alertify.success('The building has been created!!');
          this.modalReference.close();
          this.getBuildingsAsTreeView();
        });
      } else {
        this.buildingService.createMainBuilding(this.building).subscribe(res => {
          this.alertify.success('The building has been created!!');
          this.modalReference.close();
          this.getBuildingsAsTreeView();
        });
      }
    }
  }

  optionTreeGrid() {
    this.contextMenuItems = [
      {
        text: "Add-Sub Item",
        iconCss: " e-icons e-add",
        target: ".e-content",
        id: "Add-Sub-Item",
      },
      {
        text: "Delete",
        iconCss: " e-icons e-delete",
        target: ".e-content",
        id: "DeleteOC",
      },
    ];
    this.toolbar = [
      "Search",
      "ExpandAll",
      "CollapseAll",
      "ExcelExport",
      "PdfExport",
    ];
    this.editing = {
      allowEditing: true,
      allowAdding: true,
      allowDeleting: true,
      mode: "Row",
    };
    this.pageSettings = { pageSize: 20 };
    this.editparams = { params: { format: "n" } };
  }

  created() {
    this.getBuildingsAsTreeView();
  }

  onService() {
    this.buildingService.currentMessage.subscribe((arg) => {
      if (arg === 200) {
        this.getBuildingsAsTreeView();
      }
    });
  }

  toolbarClick(args) {
    switch (args.item.text) {
      // case "Add":
      //   args.cancel = true;
      //   this.openMainModal();
      //   break;
      case "PDF Export":
        this.treeGridObj.pdfExport({ hierarchyExportMode: "All" });
        break;
      case "Excel Export":
        this.treeGridObj.excelExport({ hierarchyExportMode: "All" });
        break;
      default:
        break;
    }
  }

  contextMenuClick(args) {
    switch (args.item.id) {
      case "DeleteOC":
        this.delete(args.rowInfo.rowData.entity);
        break;
      case "Add-Sub-Item":
        this.openSubModal();
        break;
      default:
        break;
    }
  }

  delete(data) {
    this.alertify.confirm(
      "Delete Building",
      'Are you sure you want to delete this Building "' + data.name + '" ?',
      () => {
        this.buildingService.delete(data.id).subscribe(
          (res) => {
            this.getBuildingsAsTreeView();
            this.alertify.success("The building has been deleted!!!");
          },
          (error) => {
            this.alertify.error("Failed to delete the building!!!");
          }
        );
      }
    );
  }

  actionComplete(args) {
    if (args.requestType === "save") {
      this.edit.name = args.data.entity.name;
      this.rename();
    }
  }

  rowSelected(args) {
    this.edit = {
      id: args.data.entity.id,
      name: args.data.entity.name,
      level: args.data.entity.level,
      parentID: args.data.entity.parentID,
    };
    this.building = {
      id: 0,
      name: "",
      parentID: args.data.entity.id,
      level: 0,
    };
  }

  getBuildingsAsTreeView() {
    this.buildingService.getBuildingsAsTreeView().subscribe((res) => {
      this.data = res;
    });
  }

  clearFrom() {
    this.building = {
      id: 0,
      name: "",
      parentID: 0,
      level: 0,
    };
  }
  rename() {
    this.buildingService.rename(this.edit).subscribe((res) => {
      this.getBuildingsAsTreeView();
      this.alertify.success("The building has been changed!!!");
    });
  }
  openMainModal() {
    this.clearFrom();
    this.modalReference = this.modalServices.open(this.content, { size: "lg"});
    this.title = "Add Main Building";
    this.building = this.building;

  }
  openSubModal() {

    this.modalReference = this.modalServices.open(this.content, {
      size: "lg",
    });
    this.title = "Add Sub Building";
    this.building = this.building;

  }

}
