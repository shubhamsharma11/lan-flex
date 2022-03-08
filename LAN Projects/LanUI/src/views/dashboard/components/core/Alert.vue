<template>
  <div name="snackbars">
    <base-material-snackbar
      v-model="show"
      :type="color"
      bottom
      right
      dismissible
    >
      {{ text }}
    </base-material-snackbar>
  </div>
</template>

<script>
  export default {
    name: 'DashboardCoreAlert',

    data: () => ({
      show: false,
      text: 'Success',
      color: 'success',
    }),

    created () {
      this.$store.subscribe((mutation, state) => {
        if (mutation.type === 'SET_ALERT') {
          this.text = state.alert.text
          this.color = state.alert.color
          this.show = state.alert.show
        }
      })
    },
  }
</script>
