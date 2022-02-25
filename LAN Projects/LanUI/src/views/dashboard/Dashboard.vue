<template>
  <v-container
    id="dashboard"
    fluid
    tag="section"
  >
    <v-row>
      <v-col
        cols="12"
        sm="6"
      >
        <base-material-stats-card
          color="#E91E63"
          icon="mdi-video"
          title="Video"
          :value="videoCount"
          sub-icon="mdi-view-list"
          sub-text="Number of videos available in the system"
          button-icon="mdi-chevron-right"
          button-action="videos"
        />
      </v-col>

      <v-col
        cols="12"
        sm="6"
      >
        <base-material-stats-card
          color="info"
          icon="mdi-music"
          title="Audio"
          :value="audioCount"
          sub-icon="mdi-playlist-music"
          sub-text="Number of audios available in the system"
          button-icon="mdi-chevron-right"
          button-action="audios"
        />
      </v-col>

      <v-col
        cols="12"
        sm="6"
      >
        <base-material-stats-card
          color="success"
          icon="mdi-image"
          title="Images"
          :value="imageCount"
          sub-icon="mdi-animation-play"
          sub-text="Number of images available in the system"
          button-icon="mdi-chevron-right"
          button-action="images"
        />
      </v-col>

      <v-col
        cols="12"
        sm="6"
      >
        <base-material-stats-card
          color="orange"
          icon="mdi-file"
          title="Total Files"
          :value="total"
          sub-icon="mdi-apps"
          sub-text="Number of total files available in the system"
        />
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  import * as axios from 'axios'
  import { variables } from '../../constants.js'

  export default {
    name: 'DashboardDashboard',

    data () {
      return {
        videoCount: 0,
        audioCount: 0,
        imageCount: 0,
        total: 0,
      }
    },

    mounted () {
      this.getStats()
    },

    methods: {
      getStats () {
        axios.get(`${variables.BASE_URL}/GetHomeStats`)
          .then(response => {
            this.videoCount = response.data.VideoCount
            this.audioCount = response.data.AudioCount
            this.imageCount = response.data.ImageCount
            this.total = response.data.TotalCount
          })
      },
    },
  }
</script>
