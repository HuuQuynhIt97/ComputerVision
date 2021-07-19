import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OcUserComponent } from './oc-user.component';

describe('OcUserComponent', () => {
  let component: OcUserComponent;
  let fixture: ComponentFixture<OcUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OcUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OcUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
