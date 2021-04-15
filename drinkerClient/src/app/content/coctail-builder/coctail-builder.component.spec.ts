import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoctailBuilderComponent } from './coctail-builder.component';

describe('CoctailBuilderComponent', () => {
  let component: CoctailBuilderComponent;
  let fixture: ComponentFixture<CoctailBuilderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CoctailBuilderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CoctailBuilderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
