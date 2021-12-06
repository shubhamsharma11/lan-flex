<template>
  <audio
    controls
    :autoplay="autoPlay"
  >
    <source
      :src="srcMp3"
      type="audio/mpeg"
    >
  </audio>
</template>

<script>

  export default {
    name: 'AudioPlayer',
    props: {

      srcMp3: {
        type: String,
        default: '/audios/test.mp3',
      },
      autoPlay: {
        type: Boolean,
        default: false,
      },
    },
  }

</script>
