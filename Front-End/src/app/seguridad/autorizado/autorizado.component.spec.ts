import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutorizadoComponent } from './autorizado.component';

describe('AutorizadoComponent', () => {
  let component: AutorizadoComponent;
  let fixture: ComponentFixture<AutorizadoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AutorizadoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AutorizadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
