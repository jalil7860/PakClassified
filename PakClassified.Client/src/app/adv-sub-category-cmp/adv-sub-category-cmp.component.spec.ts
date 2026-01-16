import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvSubCategoryCMPComponent } from './adv-sub-category-cmp.component';

describe('AdvSubCategoryCMPComponent', () => {
  let component: AdvSubCategoryCMPComponent;
  let fixture: ComponentFixture<AdvSubCategoryCMPComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdvSubCategoryCMPComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdvSubCategoryCMPComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
