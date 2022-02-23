<template>
  <v-container
    fluid
  >
    <v-row v-if="audioSrc.FilePath">
      <v-col
        cols="12"
        class="audio-player"
      >
        <v-card>
          <v-card-text>
            <audio-player
              :source="audioSrc.FilePath"
              :controls="controls"
            />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row
      v-if="audioList && audioList.length !== 0"
      style="margin-top:10px"
    >
      <v-col
        v-for="audio in audioList"
        :key="audio.FileId"
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
              {{ audio.Name }}
            </h3>
          </v-card-text>

          <template v-slot:actions>
            <v-spacer />
            <v-btn
              text
              x-small
              class="primary"
              @click="audioSrc=audio"
            >
              Play Audio
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
  import AudioPlayer from './dashboard/components/Player/AudioPlayer.vue'
  import * as axios from 'axios'
  import { variables } from '../constants.js'

  export default {
    name: 'Audios',

    components: {
      AudioPlayer,
    },

    data: () => ({
      audioList: [],
      audioSrc: {},
      dialog: false,
    }),

    mounted () {
      this.getAudios()
    },

    methods: {
      getAudios () {
        axios.get(`${variables.BASE_URL}/GetAudios`)
          .then(response => {
            this.audioList = response.data
            console.log(this.audioList)
            let count = 1
            this.audioList.forEach(el => {
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
  .audio-player
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
