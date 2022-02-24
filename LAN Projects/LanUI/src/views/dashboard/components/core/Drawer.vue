<template>
  <v-navigation-drawer
    id="core-navigation-drawer"
    v-model="drawer"
    :dark="barColor !== 'rgba(228, 226, 226, 1), rgba(255, 255, 255, 0.7)'"
    :src="barImage"
    mobile-break-point="960"
    app
    width="260"
    v-bind="$attrs"
  >
    <template v-slot:img="props">
      <v-img
        :gradient="`to bottom, ${barColor}`"
        v-bind="props"
      />
    </template>

    <v-list
      expand
      nav
    >
      <template v-for="(item, i) in computedItems">
        <base-item-group
          v-if="item.children"
          :key="`group-${i}`"
          :item="item"
        >
        </base-item-group>

        <base-item
          v-else
          :key="`item-${i}`"
          :item="item"
        />
      </template>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
  // Utilities
  import {
    mapState,
  } from 'vuex'

  export default {
    name: 'DashboardCoreDrawer',

    props: {
      expandOnHover: {
        type: Boolean,
        default: false,
      },
    },

    data: () => ({
      items: [
        {
          icon: 'mdi-view-dashboard',
          title: 'dashboard',
          to: '/',
        },
        {
          icon: 'mdi-video',
          title: 'Videos',
          to: '/videos',
        },
        {
          icon: 'mdi-music',
          title: 'Audios',
          to: '/audios',
        },
        {
          icon: 'mdi-image',
          title: 'Images',
          to: '/images',
        },
        {
          icon: 'mdi-arrow-up-thick',
          title: 'Upload',
          to: '/upload',
        },
        {
          icon: 'mdi-account',
          title: 'user',
          to: '/pages/user',
        },
        {
          title: 'typography',
          icon: 'mdi-format-font',
          to: '/components/typography',
        },
        {
          title: 'icons',
          icon: 'mdi-chart-bubble',
          to: '/components/icons',
        },
        {
          title: 'google',
          icon: 'mdi-map-marker',
          to: '/maps/google-maps',
        },
        {
          title: 'notifications',
          icon: 'mdi-bell',
          to: '/components/notifications',
        },
      ],
    }),

    computed: {
      ...mapState(['barColor', 'barImage']),
      drawer: {
        get () {
          return this.$store.state.drawer
        },
        set (val) {
          this.$store.commit('SET_DRAWER', val)
        },
      },
      computedItems () {
        return this.items.map(this.mapItem)
      },
      profile () {
        return {
          avatar: true,
        }
      },
    },

    methods: {
      mapItem (item) {
        return {
          ...item,
          children: item.children ? item.children.map(this.mapItem) : undefined,
        }
        // From Line number 21
        // <v-divider class="mb-1" />
        // <v-list
        //   dense
        //   nav
        // >
        //   <v-list-item>
        //     <v-list-item-avatar
        //       class="align-self-center"
        //       color="white"
        //       contain
        //     >
        //       <v-img
        //         src="/images/favicon.ico"
        //         max-height="30"
        //       />
        //     </v-list-item-avatar>

        //     <v-list-item-content>
        //       <v-list-item-title
        //         class="display-1"
        //         v-text="profile.title"
        //       />
        //     </v-list-item-content>
        //   </v-list-item>
        // </v-list>
        // <v-divider class="mb-2" />
      },
    },
  }
</script>
