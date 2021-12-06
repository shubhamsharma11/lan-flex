<template>
  <v-app>
    <dashboard-core-notification />

    <dashboard-core-app-bar />

    <dashboard-core-drawer />

    <v-content>
      <router-view />
    </v-content>

    <dashboard-core-settings />

    <dashboard-core-footer />
  </v-app>
</template>

<script>
  export default {
    name: 'App',

    components: {
      DashboardCoreAppBar: () => import('./views/dashboard/components/core/AppBar'),
      DashboardCoreDrawer: () => import('./views/dashboard/components/core/Drawer'),
      DashboardCoreSettings: () => import('./views/dashboard/components/core/Settings'),
      DashboardCoreNotification: () => import('./views/dashboard/components/core/Notification'),
      DashboardCoreFooter: () => import('./views/dashboard/components/core/Footer'),
    },

    data: () => ({
      expandOnHover: false,
    }),
  }
</script>
