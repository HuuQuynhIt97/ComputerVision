import { TestBed } from '@angular/core/testing';

import { BuildingUserService } from './building-user.service';

describe('BuildingUserService', () => {
  let service: BuildingUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BuildingUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
