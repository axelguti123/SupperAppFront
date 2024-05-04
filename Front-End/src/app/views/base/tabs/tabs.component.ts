import { Component } from '@angular/core';

@Component({
  selector: 'app-tabs',
  templateUrl: './tabs.component.html',
  styleUrls: ['./tabs.component.scss'],
})
export class TabsComponent {
  public panes = [
    { name: 'Partida' },
    { name: 'Presupuesto' }
  ];

  activePane = 0;

  onTabChange($event: number) {
    this.activePane = $event;
    console.log('onTabChange', $event);
  }
}
