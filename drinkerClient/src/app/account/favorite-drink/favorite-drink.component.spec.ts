import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoriteDrinkComponent } from './favorite-drink.component';

describe('FavoriteDrinkComponent', () => {
  let component: FavoriteDrinkComponent;
  let fixture: ComponentFixture<FavoriteDrinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FavoriteDrinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoriteDrinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
