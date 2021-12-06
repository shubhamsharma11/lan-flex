<template>
  <video
    controls
    :autoplay="autoPlay"
    :src="src"
    :title="title"
    :cover="cover"
    :loop="loop"
    :playsinline="playsinline"
  />
</template>

<script>

  export default {
    name: 'VideoPlayer',
    props: {
      src: {
        type: String,
        default: '',
      },
      autoPlay: {
        type: Boolean,
        default: false,
      },
      loop: {
        type: Boolean,
        default: false,
      },
      playsinline: {
        type: Boolean,
        default: false,
      },
      title: {
        type: String,
        default: '',
      },
      cover: {
        type: String,
        default: '',
      },
    },
  }

</script>
