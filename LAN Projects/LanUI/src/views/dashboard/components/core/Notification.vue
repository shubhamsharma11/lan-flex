<template>

  <base-material-snackbar
    v-model="state"
    :type="color"
    bottom
    right
  >
    {{ message }}
  </base-material-snackbar>

</template>

<script>
  export default {
    name: 'DashboardCoreNotification',

    computed: {
      color () {
        return this.$store.state.notification.color
      },
      message () {
        return this.$store.state.notification.message
      },
      state () {
        return this.$store.state.notification.state
      },
    },
  }
</script>
