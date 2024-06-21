import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss'
})
export class ButtonComponent {
  @Input() buttonText;
  @Input() buttonClass;
  @Output() buttonClick=new EventEmitter<void>();
  @Input() data: any;
  @Input() index: number;
  onClick(){
    this.buttonClick.emit(this.data);
  }
  
}
