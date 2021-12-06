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

    // data: () => ({
    //   direction: 'bottom right',
    // }),
    // parsedDirection () {
    //   return this.direction.split(' ')
    // },
    // methods: {
    //   randomColor () {
    //     this.color = this.colors[Math.floor(Math.random() * this.colors.length)]
    //     this.direction = this.directions[Math.floor(Math.random() * this.directions.length)]
    //   },
    // },
  }
</script>
