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

    <v-row
      v-if="videoList && videoList.length !== 0"
      style="margin-top:10px"
    >
      <v-col
        v-for="video in videoList"
        :key="video.FileId"
        class="col-xs-12"
        sm="4"
        md="3"
      >
        <base-material-card
          icon="mdi-music"
          color="primary"
        >
          <v-card-text>
            <h3
              class="card-title font-weight-light"
              style="text-overflow: ellipsis; white-space: nowrap; overflow: hidden;"
            >
              {{ video.Name }}
            </h3>
          </v-card-text>

          <template v-slot:actions>
            <v-spacer />
            <v-btn
              text
              x-small
              class="primary"
              @click="videoSrc=video"
            >
              Play Video
            </v-btn>
          </template>
        </base-material-card>
      </v-col>
    </v-row>

    <base-v-component
      v-else
      heading="There is nothing to show here"
      description="Please upload the a file from "
      link="upload"
    />
  </v-container>
</template>

<script>
  import VideoPlayer from './dashboard/components/Player/VideoPlayer.vue'
  import * as axios from 'axios'
  import { variables } from '../constants.js'

  export default {
    name: 'Audios',

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
        axios.get(`${variables.BASE_URL}/GetAudios`)
          .then(response => {
            this.videoList = response.data
            console.log(this.videoList)
            let count = 1
            this.videoList.forEach(el => {
              if (count === variables.THUMB_COUNT) {
                count = 1
              }
              el.FilePath = variables.FILE_URL + '/' + el.FilePath
              el.ThumbPath = variables.FILE_URL + '/thumbs/' + count + '.jpg'
              count++
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
  .v-card--material-stats
  {
    display: flex;
    flex-wrap: wrap;
    position: relative;
  }
</style>
