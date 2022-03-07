<template>
  <div name="snackbars">
    <v-snackbar
      v-model="show"
      :color="color"
      :timeout="timeout"
      bottom
      right
    >
      {{ text }}
    </v-snackbar>
  </div>
</template>

<script>
  export default {
    name: 'DashboardCoreAlert',

    data: () => ({
      show: false,
      text: 'Success',
      color: 'success',
      timeout: 10000,
    }),

    created () {
      this.$store.subscribe((mutation, state) => {
        if (mutation.type === 'SET_ALERT') {
          this.text = state.alert.text
          this.color = state.alert.color
          this.timeout = state.alert.timeout
          this.show = state.alert.show
        }
      })
    },
  }
</script>
