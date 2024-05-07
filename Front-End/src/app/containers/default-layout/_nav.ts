import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/dashboard',
    iconComponent: { name: 'cil-speedometer' }
  },
  {
    title: true,
    name: 'Usuarios'
  },
  {
    name: 'User',
    url: '/especialidad/user',
    iconComponent: { name: 'cil-drop' }
  },
  {
    name: 'Especialidad',
    url: '/especialidad/especialidad',
    linkProps: { fragment: 'someAnchor' },
    iconComponent: { name: 'cil-pencil' }
  },
  {
    name: 'Partida',
    title: true,
  },
  
  
  {
    name: 'Partida',
    url: '/partida/partida',
    iconComponent: { name: 'cil-chart-pie' }
  },
  
  {
    name: 'Seguimiento',
    url: '/partida/seguimiento',
    iconComponent: { name: 'cil-puzzle' },
  }
  
];
