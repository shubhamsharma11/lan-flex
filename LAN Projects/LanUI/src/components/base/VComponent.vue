<template>
  <section class="mb-12 text-center">
    <h1
      class="font-weight-light mb-2 headline"
      v-text="`${heading}`"
    />

    <span
      class="font-weight-light subtitle-1"
    >
      {{description}}
      <v-btn
        class="primary"
        rounded
        small
        :to="link"
      >
        Link
      </v-btn>
    </span>
  </section>
</template>

<script>
  export default {
    name: 'VComponent',

    props: {
      heading: {
        type: String,
        default: '',
      },
      description: {
        type: String,
        default: '',
      },
      link: {
        type: String,
        default: '',
      },
    },
  }
</script>
