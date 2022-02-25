<template>
  <div>
    <video
      :width="width"
      :height="height"
      :controls="controls"
      :autoplay="autoPlay"
      :src="source"
      :title="title"
      :cover="cover"
      :loop="loop"
      :playsinline="playsinline"
    />
    <div class="display-2 font-weight-light mt-2">
      {{ title }}
    </div>
  </div>
</template>

<script>

  export default {
    name: 'VideoPlayer',
    props: {
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
      controls: {
        type: Boolean,
        default: true,
      },
      width: {
        type: String,
        default: '100%',
      },
      height: {
        type: String,
        default: '100%',
      },
    },
  }

</script>
