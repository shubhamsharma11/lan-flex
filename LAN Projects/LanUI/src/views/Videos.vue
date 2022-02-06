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
        class="col-xs-12"
        sm="4"
        md="3"
      >
        <v-card @click="videoSrc=video">
          <v-card-title>
            <img
              :src="video.ThumbPath"
              style="border-radius: 5px; width: 100%; height: 250px;"
            >
          </v-card-title>
          <v-card-text>
            <h4
              class="card-title font-weight-light mt-2 ml-2"
              style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden;"
            >
              {{ video.Name }}
            </h4>
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

    mounted () {
      this.getVideos()
    },

    methods: {
      getVideos () {
        axios.get(`${variables.BASE_URL}/GetVideos`)
          .then(response => {
            this.videoList = response.data

            this.videoList.forEach(el => {
              const random = Math.floor((Math.random() * 13) + 1)
              el.FilePath = variables.FILE_URL + '/' + el.FilePath
              el.ThumbPath = variables.FILE_URL + '/thumbs/' + random + '.jpg'
            })
          })
      },
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
