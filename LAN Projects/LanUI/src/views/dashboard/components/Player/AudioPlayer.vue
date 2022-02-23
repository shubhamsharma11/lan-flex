<template>
  <div>
    <audio
      :controls="controls"
      :autoplay="autoPlay"
      :src="source"
      :loop="loop"
      style="width: 100%; height: 30px;"
    />
  </div>
</template>

<script>

  export default {
    name: 'AudioPlayer',
    props: {
      source: {
        type: String,
        default: '',
      },
      autoPlay: {
        type: Boolean,
        default: true,
      },
      loop: {
        type: Boolean,
        default: false,
      },
      controls: {
        type: Boolean,
        default: true,
      },
    },
  }

</script>
