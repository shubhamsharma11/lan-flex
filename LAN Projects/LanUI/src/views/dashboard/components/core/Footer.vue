<template>
  <v-footer
    padless
  >
    <v-row
      justify="center"
      no-gutters
    >
      <v-col
        class="text-center"
        cols="12"
      >
        <v-btn
          v-for="(s, i) in social"
          :key="i"
          icon
          elevation="20"
          class="pt-1 ma-1"
        >
          <v-icon
            :color="s.color"
            v-text="s.icon"
          />
        </v-btn>
      </v-col>
      <v-btn
        v-for="link in links"
        :key="link"
        text
        rounded
      >
        {{ link.text }}
      </v-btn>
      <v-col
        class="text-center"
        cols="12"
      >
        &copy; {{ new Date().getFullYear() }} — <strong>Made with <v-icon color="primary">mdi-heart</v-icon> by Shubham</strong>
      </v-col>
    </v-row>
  </v-footer>
</template>

<script>
  export default {
    name: 'DashboardCoreFooter',

    data: () => ({
      links: [
        {
          href: '#',
          text: 'Creative Tim',
        },
        {
          href: '#',
          text: 'About Us',
        },
        {
          href: '#',
          text: 'Blog',
        },
        {
          href: '#',
          text: 'Licenses',
        },
      ],
      social: [
        {
          color: '#55ACEE',
          icon: 'mdi-twitter',
        },
        {
          color: '#3B5998',
          icon: 'mdi-facebook',
        },
        {
          color: '#DD4b39',
          icon: 'mdi-google-plus',
        },
        {
          color: '#0976B4',
          icon: 'mdi-linkedin',
        },
        {
          color: '#CC2127',
          icon: 'mdi-pinterest',
        },
        {
          color: '#E52D27',
          icon: 'mdi-youtube',
        },
        {
          color: '#333333',
          icon: 'mdi-github',
        },
        {
          color: '#e94e31',
          icon: 'mdi-git',
        },
        {
          color: '#db4128',
          icon: 'mdi-gitlab',
        },
        {
          color: '#FF4500',
          icon: 'mdi-reddit',
        },
      ],
    }),
  }
</script>

<style lang="sass">
  #dashboard-core-footer
    a
      font-size: .825rem
      font-weight: 500
      text-decoration: none
      text-transform: uppercase
</style>
