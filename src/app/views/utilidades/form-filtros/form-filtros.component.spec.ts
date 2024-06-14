import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormFiltrosComponent } from './form-filtros.component';

describe('FormFiltrosComponent', () => {
  let component: FormFiltrosComponent;
  let fixture: ComponentFixture<FormFiltrosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FormFiltrosComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(FormFiltrosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
