import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavLayout } from './nav-layout';

describe('NavLayout', () => {
  let component: NavLayout;
  let fixture: ComponentFixture<NavLayout>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [NavLayout],
    }).compileComponents();

    fixture = TestBed.createComponent(NavLayout);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
