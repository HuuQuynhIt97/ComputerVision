import { INavData } from '@coreui/angular';

export const navItems: INavData[] = [
  {
    name: 'Dashboard',
    url: '/ess/todolist',
    icon: 'icon-speedometer',
  },

  // {
  //   title: true,
  //   name: 'Maintainance'
  // },
  // {
  //   name: 'Brand',
  //   url: '/maintenance/brand',
  //   icon: 'icon-bell',
  // },
  // {
  //   name: 'Audit Type',
  //   url: '/maintenance/audit-type',
  //   icon: 'icon-list',
  // },

  // {
  //   title: true,
  //   name: 'Components'
  // },

  {
    name: 'Setting',
    url: '/ess',
    icon: 'fa fa-cog',
    children: [
      // {
      //   name: 'Account',
      //   url: '/ess/setting/account',
      //   icon: 'fa fa-circle-thin'
      // },

      // {
      //   name: 'OC',
      //   url: '/ess/setting/oc',
      //   icon: 'fa fa-circle-thin'
      // },
      // {
      //   name: 'OC User',
      //   url: '/ess/setting/oc-user',
      //   icon: 'fa fa-circle-thin'
      // },

    ]
  },

  // {
  //   name: 'Page For Manager',
  //   url: '/buttons',
  //   icon: 'icon-speedometer',
  // },

  // {
  //   name: 'Page For Supervisor',
  //   url: '/notifications',
  //   icon: 'icon-speedometer',
  // },

  {
    name: 'To Do List',
    url: '/ess/todolist',
    icon: 'fa fa-list-ul',
  },
  {
    name: 'Work Plan',
    url: '/ess/work-plan',
    icon: 'fa fa-newspaper-o',
  },

  // {
  //   name: 'Buttons',
  //   url: '/buttons',
  //   icon: 'icon-cursor',
  //   children: [
  //     {
  //       name: 'Buttons',
  //       url: '/buttons/buttons',
  //       icon: 'icon-cursor'
  //     },
  //     {
  //       name: 'Dropdowns',
  //       url: '/buttons/dropdowns',
  //       icon: 'icon-cursor'
  //     },
  //     {
  //       name: 'Brand Buttons',
  //       url: '/buttons/brand-buttons',
  //       icon: 'icon-cursor'
  //     }
  //   ]
  // },
  // {
  //   name: 'Charts',
  //   url: '/charts',
  //   icon: 'icon-pie-chart'
  // },
  // {
  //   name: 'Icons',
  //   url: '/icons',
  //   icon: 'icon-star',
  //   children: [
  //     {
  //       name: 'CoreUI Icons',
  //       url: '/icons/coreui-icons',
  //       icon: 'icon-star',
  //       badge: {
  //         variant: 'success',
  //         text: 'NEW'
  //       }
  //     },
  //     {
  //       name: 'Flags',
  //       url: '/icons/flags',
  //       icon: 'icon-star'
  //     },
  //     {
  //       name: 'Font Awesome',
  //       url: '/icons/font-awesome',
  //       icon: 'icon-star',
  //       badge: {
  //         variant: 'secondary',
  //         text: '4.7'
  //       }
  //     },
  //     {
  //       name: 'Simple Line Icons',
  //       url: '/icons/simple-line-icons',
  //       icon: 'icon-star'
  //     }
  //   ]
  // },
  // {
  //   name: 'Notifications',
  //   url: '/notifications',
  //   icon: 'icon-bell',
  //   children: [
  //     {
  //       name: 'Alerts',
  //       url: '/notifications/alerts',
  //       icon: 'icon-bell'
  //     },
  //     {
  //       name: 'Badges',
  //       url: '/notifications/badges',
  //       icon: 'icon-bell'
  //     },
  //     {
  //       name: 'Modals',
  //       url: '/notifications/modals',
  //       icon: 'icon-bell'
  //     }
  //   ]
  // },
  // {
  //   name: 'Widgets',
  //   url: '/widgets',
  //   icon: 'icon-calculator',
  //   badge: {
  //     variant: 'info',
  //     text: 'NEW'
  //   }
  // },
  // {
  //   divider: true
  // },
  // {
  //   title: true,
  //   name: 'Extras',
  // },
  // {
  //   name: 'Pages',
  //   url: '/pages',
  //   icon: 'icon-star',
  //   children: [
  //     {
  //       name: 'Login',
  //       url: '/login',
  //       icon: 'icon-star'
  //     },
  //     {
  //       name: 'Register',
  //       url: '/register',
  //       icon: 'icon-star'
  //     },
  //     {
  //       name: 'Error 404',
  //       url: '/404',
  //       icon: 'icon-star'
  //     },
  //     {
  //       name: 'Error 500',
  //       url: '/500',
  //       icon: 'icon-star'
  //     }
  //   ]
  // },
  // {
  //   name: 'Disabled',
  //   url: '/dashboard',
  //   icon: 'icon-ban',
  //   badge: {
  //     variant: 'secondary',
  //     text: 'NEW'
  //   },
  //   attributes: { disabled: true },
  // }
];
