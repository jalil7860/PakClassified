import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubcateListingsComponent } from './subcate-listings.component';

describe('SubcateListingsComponent', () => {
  let component: SubcateListingsComponent;
  let fixture: ComponentFixture<SubcateListingsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SubcateListingsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubcateListingsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
