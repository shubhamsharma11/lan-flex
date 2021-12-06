<template>
  <v-container
    fluid
  >
    <base-v-component
      heading="Simple Tables"
      link="components/simple-tables"
    />
    <video-player />
    <div class="py-3" />
    <audio-player />
  </v-container>
</template>

<script>
  import VideoPlayer from '../components/Player/VideoPlayer.vue'
  import AudioPlayer from '../components/Player/AudioPlayer.vue'

  export default {
    components: {
      VideoPlayer,
      AudioPlayer,
    },
  }
</script>
