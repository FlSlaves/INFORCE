import { ComponentFixture, TestBed } from '@angular/core/testing';

import { URLtableComponent } from './urltable.component';

describe('URLtableComponent', () => {
  let component: URLtableComponent;
  let fixture: ComponentFixture<URLtableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ URLtableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(URLtableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
