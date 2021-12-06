<template>
  <video-player />
</template>

<script>
  import VideoPlayer from './components/Player/VideoPlayer.vue'

  export default {
    name: 'DashboardDashboard',
    components: {
      VideoPlayer,
    },
  }
</script>

<style lang="sass">
  #upgrade
    .v-data-table
      th, td
        border: none !important
</style>
