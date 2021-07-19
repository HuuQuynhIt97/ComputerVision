import { BuildingUserService } from './../../../../_core/_services/building-user.service';
import { BuildingService } from './../../../../_core/_services/building.service';
import { AlertifyService } from './../../../../_core/_services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../../_core/_services/account.service';
@Component({
  selector: 'app-oc-user',
  templateUrl: './oc-user.component.html',
  styleUrls: ['./oc-user.component.css']
})
export class OcUserComponent implements OnInit {

  toolbarBuilding: object;
  toolbarUser: object;
  data: any;
  buildingID: number;
  userData: any;
  buildingUserData: any;
  constructor(
    private buildingService: BuildingService,
    private alertify: AlertifyService,
    private buildingUserService: BuildingUserService,
    private accountService: AccountService,
  ) { }

  ngOnInit() {
    this.toolbarUser = ['Search'];
  }

  created() {
    this.getBuildingsAsTreeView();
  }

  createdUsers() {
    // this.getAllUsers();
  }

  contextMenuClick(args) { }

  actionComplete(args) { }

  rowSelected(args) {
    const data = args.data.entity || args.data;
    this.buildingID = Number(data.id);
    if (args.isInteracted) {
      this.getBuildingUserByBuildingID(this.buildingID);
    }
  }

  getBuildingsAsTreeView() {
    this.buildingService.getBuildingsAsTreeView().subscribe(res => {
      this.data = res;
    });
  }

  mappingUserWithBuilding(obj) {
    this.buildingUserService.mappingUserWithBuilding(obj).subscribe((res: any) => {
      if (res.status) {
        this.alertify.success(res.message);
        // this.getBuildingUserByBuildingID(this.buildingID);
      } else {
        this.alertify.warning(res.message);
        this.getBuildingUserByBuildingID(this.buildingID);
      }
    });
  }

  removeBuildingUser(obj) {
    this.buildingUserService.removeBuildingUser(obj).subscribe((res: any) => {
      if (res.status) {
        this.alertify.success(res.message);
      } else {
        this.alertify.warning(res.message);
      }
    });
  }

  getAllUsers() {
    this.accountService.getAll().subscribe((res: any) => {
      const data = res.map((i: any) => {
        return {
          ID: i.id,
          Username: i.username,
          Email: i.email,
          Status: this.checkStatus(i.id)
        };
      });
      this.userData = data;
    });
  }

  getBuildingUserByBuildingID(buildingID) {
    this.buildingUserService.getBuildingUserByBuildingID(buildingID).subscribe(res => {
      this.buildingUserData = res || [];
      this.getAllUsers();
    });
  }

  checkStatus(userID) {

    this.buildingUserData = this.buildingUserData || [];
    const item = this.buildingUserData.filter(i => {
      return i.userID === userID && i.buildingID === this.buildingID;
    });
    if (item.length <= 0) {
      return false;
    } else {
      return true;
    }

  }

  onChangeMap(args, data) {
    if (this.buildingID > 0) {
      if (args.checked) {
        const obj = {
          userID: data.ID,
          buildingID: this.buildingID
        };
        this.mappingUserWithBuilding(obj);
      } else {
        const obj = {
          userID: data.ID,
          buildingID: this.buildingID
        }
        this.removeBuildingUser(obj);
      }
    } else {
      this.alertify.warning('Please select a building!');
    }
  }

}
