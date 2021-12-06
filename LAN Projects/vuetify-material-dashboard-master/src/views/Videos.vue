<template>
  <v-container
    fluid
  >
    <v-row>
      <v-spacer />
      <v-btn
        icon
        text
        large
        @click="dialog = true"
      >
        <v-icon>
          mdi-upload
        </v-icon>
      </v-btn>
    </v-row>
    <v-row v-if="videoSrc.src">
      <v-col
        cols="12"
        class="video-player"
      >
        <v-card>
          <v-card-text>
            <video-player
              style="width: 100%"
              :src="videoSrc.src"
              :auto-play="true"
              :title="videoSrc.name"
            />
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>

    <v-row>
      <v-col
        v-for="video in videoList"
        :key="video.id"
        class="col-xs-6"
        sm="2"
      >
        <v-card @click="videoSrc=video">
          <v-card-title>
            <img
              :src="video.thumb"
              style="border-radius: 5px; width: 100%"
            >
          </v-card-title>
          <v-card-text>
            <h4 class="card-title font-weight-light mt-2 ml-2">
              {{ video.name }}
            </h4>

            <p class="d-inline-flex font-weight-light ml-2 mt-1">
              {{ video.details }}
            </p>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
    <v-dialog
      v-model="dialog"
      max-width="500"
      persistent
    >
      <v-card>
        <v-card-title>
          Upload a video
        </v-card-title>
        <v-card-text>
          <!--UPLOAD-->
          <div class="dropbox">
            <input
              type="file"
              multiple
              :name="uploadFieldName"
              :disabled="isSaving"
              @change="onSelectVideo"
              accept="video/*"
              class="input-file"
            >
            <p v-if="isInitial || !selectedFiles">
              Drag your file(s) here to begin<br> or click to browse
            </p>
            <p v-if="isSaving">
              Uploading {{ fileCount }} files...
            </p>
            <ul v-if="selectedFiles.length > 0">
              Selected Files
              <li
                v-for="item in selectedFiles"
                :key="item.name"
              >
                {{ item.name }}
              </li>
            </ul>
          </div>
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn
            color="info"
            outlined
            @click="onUpload"
            :disabled="selectedFiles.length == 0"
          >
            Upload
          </v-btn>
          <v-btn
            color="error"
            outlined
            @click="dialog = false"
          >
            Cancel
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
  import VideoPlayer from './dashboard/components/Player/VideoPlayer.vue'
  import VideoList from '../constants.json'
  import { upload } from '../services/FileUploadService.js'

  const STATUS_INITIAL = 0
  const STATUS_SAVING = 1
  const STATUS_SUCCESS = 2
  const STATUS_FAILED = 3

  export default {
    name: 'Videos',

    components: {
      VideoPlayer,
    },

    data: () => ({
      videoList: VideoList,
      videoSrc: {},
      dialog: false,

      uploadedFiles: [],
      uploadError: null,
      currentStatus: null,
      uploadFieldName: 'videos',
      selectedFiles: [],
      formData: FormData,
    }),

    computed: {
      isInitial () {
        return this.currentStatus === STATUS_INITIAL
      },
      isSaving () {
        return this.currentStatus === STATUS_SAVING
      },
      isSuccess () {
        return this.currentStatus === STATUS_SUCCESS
      },
      isFailed () {
        return this.currentStatus === STATUS_FAILED
      },
    },

    methods: {
      onSelectVideo ($event) {
        this.selectedFiles = $event.target.files
      },
      onUpload () {
        this.formData = new FormData()

        if (!this.selectedFiles.length) return

        // append the files to FormData
        this.selectedFiles.forEach(file => {
          this.formData.append(this.uploadFieldName, file, file.name)
        })
      },

      reset () {
        // reset form to initial state
        this.currentStatus = STATUS_INITIAL
        this.uploadedFiles = []
        this.uploadError = null
      },
      save () {
        // upload data to the server
        this.currentStatus = STATUS_SAVING

        upload(this.formData)
          .then(x => {
            this.uploadedFiles = [].concat(x)
            this.currentStatus = STATUS_SUCCESS
          })
          .catch(err => {
            this.uploadError = err.response
            this.currentStatus = STATUS_FAILED
          })
      },
    },
    mounted () {
      this.reset()
    },
  }
</script>
<style>
  .v-card
  {
    padding: 5px;
    margin: 0px;
  }
  .video-player
  {
    padding: 0px;
  }

  .dropbox {
    color: dimgray;
    padding: 10px 10px;
    min-height: 200px; /* minimum height */
    position: relative;
    cursor: pointer;
    border: 2px dashed gray; /* the dash box */
    border-radius: 5px;
  }

  .input-file {
    opacity: 0; /* invisible but it's there! */
    width: 100%;
    height: 200px;
    position: absolute;
    cursor: pointer;
  }

  .dropbox:hover {
    background: rgb(245, 245, 245); /* when mouse over to the drop zone, change color */
  }

  .dropbox p {
    font-size: 1.2em;
    text-align: center;
    padding: 50px 0;
  }
</style>
