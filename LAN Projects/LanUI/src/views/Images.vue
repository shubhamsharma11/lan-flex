<template>
  <v-container
    fluid
  >
    <v-row justify="center">
      <v-dialog
        v-model="dialog"
        width="50%"
        persistent
      >
        <v-card>
          <v-toolbar
            flat
            dark
            color="primary"
          >
            <v-toolbar-title>{{ imageList[selectedImage].Name }}</v-toolbar-title>
            <v-spacer />
            <v-btn
              icon
              dark
              @click="dialog = false"
            >
              <v-icon>mdi-close</v-icon>
            </v-btn>
          </v-toolbar>
          <v-window
            v-model="selectedImage"
            show-arrows
          >
            <v-window-item
              v-for="(img, i) in imageList"
              :key="i"
            >
              <v-img
                :src="img.FilePath"
                class="grey lighten-2"
                contain
                height="400px"
              >
                <template v-slot:placeholder>
                  <v-row
                    class="fill-height ma-0"
                    align="center"
                    justify="center"
                  >
                    <v-progress-circular
                      indeterminate
                      color="grey lighten-5"
                    ></v-progress-circular>
                  </v-row>
                </template>
              </v-img>
            </v-window-item>
          </v-window>
        </v-card>
      </v-dialog>
    </v-row>

    <v-row
      v-if="imageList && imageList.length !== 0"
      style="margin-top:10px"
    >
      <v-col
        v-for="(image, i) in imageList"
        :key="image.FileId"
        class="d-flex child-flex"
        sm="3"
        md="2"
      >
        <v-card
          @click="dialog = true; selectedImage = i"
        >
          <v-img
            :src="image.FilePath"
            aspect-ratio="1"
            class="grey lighten-2"
          >
            <template v-slot:placeholder>
              <v-row
                class="fill-height ma-0"
                align="center"
                justify="center"
              >
                <v-progress-circular
                  indeterminate
                  color="grey lighten-5"
                ></v-progress-circular>
              </v-row>
            </template>
          </v-img>
          <v-card-title class="text-h6">
            {{ image.Name }}
          </v-card-title>
        </v-card>
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
  import * as axios from 'axios'
  import { variables } from '../constants.js'

  export default {
    name: 'Images',

    data: () => ({
      imageList: [],
      imageSrc: {},
      dialog: false,
      selectedImage: 0,
    }),

    mounted () {
      this.getImages()
    },

    methods: {
      getImages () {
        axios.get(`${variables.BASE_URL}/GetImages`)
          .then(response => {
            this.imageList = response.data
            let count = 1
            this.imageList.forEach(el => {
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
