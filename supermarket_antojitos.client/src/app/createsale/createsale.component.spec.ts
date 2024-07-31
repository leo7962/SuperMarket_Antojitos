import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatesaleComponent } from './createsale.component';

describe('CreatesaleComponent', () => {
  let component: CreatesaleComponent;
  let fixture: ComponentFixture<CreatesaleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CreatesaleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreatesaleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
