<template>
  <v-app-bar
    id="app-bar"
    absolute
    app
    height="60"
    :dark="barColor !== 'rgba(228, 226, 226, 1), rgba(255, 255, 255, 0.7)'"
    :src="barImage"
  >
    <template v-slot:img="{ props }">
      <v-img
        v-bind="props"
        :gradient="`to bottom, ${barColor}`"
      />
    </template>

    <v-btn
      class="mr-3"
      elevation="1"
      fab
      small
      @click="setDrawer(!drawer)"
    >
      <v-icon v-if="value">
        mdi-view-quilt
      </v-icon>

      <v-icon v-else>
        mdi-dots-vertical
      </v-icon>
    </v-btn>

    <v-toolbar-title
      class="hidden-sm-and-down font-weight-light"
      v-text="$route.name"
    />

    <v-spacer />

    <v-text-field
      label="Search"
      color="primary"
      hide-details
      style="max-width: 165px;"
    >
      <template
        v-if="$vuetify.breakpoint.mdAndUp"
        v-slot:append-outer
      >
        <v-btn
          class="mt-n2"
          elevation="1"
          fab
          small
        >
          <v-icon>mdi-magnify</v-icon>
        </v-btn>
      </template>
    </v-text-field>

    <div class="mx-3" />

    <v-btn
      class="ml-2"
      min-width="0"
      text
      to="/"
    >
      <v-icon>mdi-view-dashboard</v-icon>
    </v-btn>

    <v-menu
      bottom
      left
      offset-y
      origin="top right"
      transition="scale-transition"
      :close-on-content-click="false"
      rounded
    >
      <template v-slot:activator="{ attrs, on }">
        <v-btn
          class="ml-2"
          min-width="0"
          text
          v-bind="attrs"
          v-on="on"
        >
          <v-badge
            color="red"
            overlap
            bordered
          >
            <template v-slot:badge>
              <span>{{ notifications.length }}</span>
            </template>

            <v-icon>mdi-bell</v-icon>
          </v-badge>
        </v-btn>
      </template>

      <v-list
        v-if="notifications.length !== 0"
        :tile="false"
        nav
        dense
      >
        <v-subheader>NOTIFICATIONS</v-subheader>
        <v-list-item-group>
          <v-list-item
            v-for="(n, i) in notifications"
            :key="`item-${i}`"
            :color="n.color"
          >
            <v-list-item-content>
              <v-list-item-title>
                {{ n.text }}
              </v-list-item-title>
            </v-list-item-content>

            <v-btn
              icon
              @click="removeMessage(i)"
            >
              <v-icon>
                mdi-close
              </v-icon>
            </v-btn>
          </v-list-item>
        </v-list-item-group>
      </v-list>
      <v-list
        v-else
      >
        <v-list-item>
          <v-list-item-title>
            No notifications yet
          </v-list-item-title>
        </v-list-item>
      </v-list>
    </v-menu>

    <v-btn
      class="ml-2"
      min-width="0"
      text
      to="/pages/user"
    >
      <v-icon>mdi-account</v-icon>
    </v-btn>
  </v-app-bar>
</template>

<script>
  // Utilities
  import { mapState, mapMutations } from 'vuex'

  export default {
    name: 'DashboardCoreAppBar',

    data: () => ({
      selectedItem: 1,
      items: [
        { text: 'Real-Time', icon: 'mdi-clock' },
        { text: 'Audience', icon: 'mdi-account' },
        { text: 'Conversions', icon: 'mdi-flag' },
      ],
    }),

    props: {
      value: {
        type: Boolean,
        default: false,
      },
    },

    computed: {
      ...mapState(['drawer', 'barColor', 'barImage', 'notifications']),
    },

    methods: {
      ...mapMutations({
        setDrawer: 'SET_DRAWER',
        removeNotification: 'SET_NOTIFICATION',
      }),
      removeMessage (key) {
        this.removeNotification(key)
      },
    },
  }
</script>
