import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    barColor: 'rgba(0, 0, 0, .8), rgba(0, 0, 0, .8)',
    barImage: '/images/sidebar-1.jpg',
    drawer: null,
    notifications: [],
    alert: null,
  },
  mutations: {
    SET_BAR_IMAGE (state, payload) {
      state.barImage = payload
    },
    SET_DRAWER (state, payload) {
      state.drawer = payload
    },
    SET_NOTIFICATION (state, payload) {
      if (typeof payload === 'object') {
        state.notifications.push(payload)
      } else {
        state.notifications.splice(payload, 1)
      }
    },
    SET_ALERT (state, payload) {
      state.alert = payload
    },
  },
})
