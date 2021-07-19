import { MainRoutingModule } from './main-routing.module';
// Angular
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
// Forms Component

// Tabs Component
import { TabsModule } from 'ngx-bootstrap/tabs';

// Carousel Component
import { CarouselModule } from 'ngx-bootstrap/carousel';

// Collapse Component
import { CollapseModule } from 'ngx-bootstrap/collapse';

// Dropdowns Component
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

// Pagination Component
import { PaginationModule } from 'ngx-bootstrap/pagination';

// Popover Component
import { PopoverModule } from 'ngx-bootstrap/popover';
import { ModalModule } from 'ngx-bootstrap/modal';
// Progress Component
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';

// Tooltip Component
// import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { AccountComponent } from './setting/account/account.component';
import { TodolistComponent } from './todolist/todolist.component';
import { OcUserComponent } from './setting/oc-user/oc-user.component';
import { OcComponent } from './setting/oc/oc.component';
//Syncfusion ej2-angular-popups module
import { TooltipModule } from '@syncfusion/ej2-angular-popups';

// Components Plugin
import { DropDownListModule } from '@syncfusion/ej2-angular-dropdowns';
import { SwitchModule, RadioButtonModule } from '@syncfusion/ej2-angular-buttons';
import { GridAllModule } from '@syncfusion/ej2-angular-grids';
import { TreeGridAllModule } from '@syncfusion/ej2-angular-treegrid';
import { ButtonModule } from '@syncfusion/ej2-angular-buttons';

import { DatePickerModule } from '@syncfusion/ej2-angular-calendars';
import { ToolbarModule } from '@syncfusion/ej2-angular-navigations';
import { CheckBoxModule } from '@syncfusion/ej2-angular-buttons';
import { TimePickerModule } from '@syncfusion/ej2-angular-calendars';
import { DateTimePickerModule } from '@syncfusion/ej2-angular-calendars';

import { NgbModalModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { FilePondModule, registerPlugin } from 'ngx-filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
import FilePondPluginImagePreview from 'filepond-plugin-image-preview';
import FilePondPluginFileValidateSize from 'filepond-plugin-file-validate-size';
registerPlugin(FilePondPluginFileValidateType,FilePondPluginFileValidateSize,FilePondPluginImagePreview);
import { NgxDocViewerModule } from 'ngx-doc-viewer';
import { NgxSpinnerModule } from 'ngx-spinner';
import { WorkPlanComponent } from './work-plan/work-plan.component';

@NgModule({
  imports: [
    NgxDocViewerModule,
    NgxSpinnerModule,
    FilePondModule,
    CommonModule,
    TreeGridAllModule,
    GridAllModule,
    DateTimePickerModule,
    TimePickerModule,
    CheckBoxModule,
    TooltipModule,
    ToolbarModule,
    DatePickerModule,
    NgbModalModule,
    NgbModule,
    ButtonModule,
    SwitchModule,
    RadioButtonModule,
    DropDownListModule,
    FormsModule,
    ModalModule.forRoot(),
    MainRoutingModule,
    BsDropdownModule.forRoot(),
    TabsModule,
    CarouselModule.forRoot(),
    CollapseModule.forRoot(),
    PaginationModule.forRoot(),
    // PopoverModule.forRoot(),
    ProgressbarModule.forRoot(),
    // TooltipModule.forRoot()
  ],
  declarations: [
  AccountComponent,
  TodolistComponent,
  OcUserComponent,
  OcComponent,
  WorkPlanComponent,
],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class MainModule { }
