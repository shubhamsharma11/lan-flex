<template>
  <v-container
    id="regular-tables"
    fluid
    tag="section"
  >
    <dashboard-pages-timeline />
  </v-container>
</template>
<script>
  import DashboardPagesTimeline from '../views/dashboard/pages/Timeline.vue'

  export default {
    name: 'Features',

    components: {
      DashboardPagesTimeline,
    },

    data: () => ({
    }),
  }
</script>
