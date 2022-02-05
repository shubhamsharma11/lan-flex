<template>
  <v-container
    fluid
  >
    <v-row v-if="videoSrc.FilePath">
      <v-col
        cols="12"
        class="video-player"
      >
        <v-card>
          <v-card-text>
            <video-player
              :source="videoSrc.FilePath"
              :type="videoSrc.Type"
              :auto-play="true"
              :title="videoSrc.Name"
            />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col
        v-for="video in videoList"
        :key="video.FileId"
        class="col-xs-6"
        sm="2"
      >
        <v-card @click="videoSrc=video">
          <v-card-title>
            <img
              :src="video.ThumbPath"
              style="border-radius: 5px; width: 100%"
            >
          </v-card-title>
          <v-card-text>
            <h4 class="card-title font-weight-light mt-2 ml-2">
              {{ video.Name }}
            </h4>

            <p class="d-inline-flex font-weight-light ml-2 mt-1">
              {{ video.Details }}
            </p>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  import VideoPlayer from './dashboard/components/Player/VideoPlayer.vue'
  import * as axios from 'axios'
  import { variables } from '../constants.js'

  export default {
    name: 'Videos',

    components: {
      VideoPlayer,
    },

    data: () => ({
      videoList: [],
      videoSrc: {},
      dialog: false,
    }),

    methods: {
      getVideos () {
        axios.get(`${variables.BASE_URL}/GetVideos`)
          .then(response => {
            this.videoList = response.data

            this.videoList.forEach(el => {
              el.FilePath = variables.FILE_URL + '/' + el.FilePath
            })
          })
      },
    },

    mounted () {
      this.getVideos()
    },
  }
</script>
<style>
  .v-card
  {
    padding: 2px;
    margin: 0px;
  }
  .video-player
  {
    padding: 0px;
  }
</style>
