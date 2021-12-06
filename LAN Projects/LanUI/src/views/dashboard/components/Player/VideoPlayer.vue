<template>
  <div>
    <video
      style="width: 100%;"
      controls
      :autoplay="autoPlay"
      :src="source"
      :title="title"
      :cover="cover"
      :loop="loop"
      :playsinline="playsinline"
    />
  </div>
</template>

<script>

  export default {
    name: 'VideoPlayer',
    props: {
      type: {
        type: String,
        default: '',
      },
      source: {
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
