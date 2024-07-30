import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MakeproductComponent } from './makeproduct.component';

describe('MakeproductComponent', () => {
  let component: MakeproductComponent;
  let fixture: ComponentFixture<MakeproductComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MakeproductComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MakeproductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
