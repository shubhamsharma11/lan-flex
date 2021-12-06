<template>
  <v-row
    align="center"
    justify="center"
  >
    <v-col
      cols="12"
      sm="8"
    >
      <base-material-card color="primary">
        <template v-slot:heading>
          <div class="display-2 font-weight-light">
            Upload File
          </div>

          <div class="subtitle-1 font-weight-light">
            Enter the details about the file being uploaded
          </div>
        </template>

        <v-form>
          <v-container class="py-0">
            <v-row>
              <!-- File Title -->
              <v-col
                cols="12"
                md="6"
              >
                <v-text-field
                  v-model="fileTitle"
                  label="File Title"
                  outlined
                  dense
                />
              </v-col>

              <!-- File Type -->
              <v-col
                cols="12"
                md="6"
              >
                <!-- This should be drop down (Video / Audio / Image) -->
                <!-- <v-text-field
                  label="File Type"
                /> -->
                <v-select
                  v-model="selectedFileType"
                  label="File Type"
                  outlined
                  dense
                  :items="fileTypes"
                  item-text="fileType"
                  item-value="fileTypeCode"
                  return-object
                />
              </v-col>

              <!-- Video Genre -->
              <v-col
                v-if="selectedFileType.fileType == 'Video'"
                cols="12"
                md="6"
              >
                <!-- This should be a dropdown -->
                <!-- <v-text-field
                  label="Genere"
                  outlined
                  dense
                /> -->
                <v-select
                  v-model="selectedVideoGenre"
                  label="Video Genre"
                  outlined
                  dense
                  :items="videoGenre"
                />
              </v-col>

              <!-- Video Type -->
              <v-col
                v-if="selectedFileType.fileType == 'Video'"
                cols="12"
                md="6"
              >
                <!-- This also should be radio buttons (movie/tv series/music video)
                This should be enable only if user selects Video in above radio button -->
                <!-- <v-text-field
                  label="Video Type"
                  outlined
                  dense
                /> -->
                <v-select
                  v-model="selectedVideoType"
                  label="Video Type"
                  outlined
                  dense
                  :items="videoTypes"
                />
              </v-col>

              <!-- Music Genre -->
              <v-col
                v-if="selectedFileType.fileType == 'Music'"
                cols="12"
                md="6"
              >
                <!-- This should be a dropdown -->
                <!-- <v-text-field
                  label="Genere"
                  outlined
                  dense
                /> -->
                <v-select
                  v-model="selectedMusicGenre"
                  label="Music Genre"
                  outlined
                  dense
                  :items="musicGenre"
                />
              </v-col>

              <!-- File Language -->
              <v-col
                v-if="selectedFileType.fileType == 'Music' || selectedFileType.fileType == 'Video'"
                cols="12"
                md="6"
              >
                <!-- This also should be select box (English Songs/Hindi Songs/Bhajans)
                This should be enable only if user selects Video in previous select box -->
                <!-- <v-text-field
                  label="Audio Type"
                  outlined
                  dense
                /> -->
                <v-select
                  v-model="selectedFileLanguage"
                  label="File Language"
                  outlined
                  dense
                  :items="fileLanguage"
                />
              </v-col>

              <!-- <v-col
                cols="12"
                md="6"
              >
                <v-text-field
                  label="Last Name"
                  class="purple-input"
                />
              </v-col>

              <v-col cols="12">
                <v-text-field
                  label="Adress"
                  class="purple-input"
                />
              </v-col>

              <v-col
                cols="12"
                md="4"
              >
                <v-text-field
                  label="City"
                  class="purple-input"
                />
              </v-col>

              <v-col
                cols="12"
                md="4"
              >
                <v-text-field
                  label="Country"
                  class="purple-input"
                />
              </v-col>

              <v-col
                cols="12"
                md="4"
              >
                <v-text-field
                  class="purple-input"
                  label="Postal Code"
                  type="number"
                />
              </v-col> -->

              <!-- Description -->
              <v-col cols="12">
                <v-textarea
                  label="Description"
                  placeholder="Enter the description for the file."
                  rows="1"
                  dense
                  outlined
                />
              </v-col>

              <!--Dropbox-->
              <!-- Make the dropbox similar to othe fields -->
              <v-col>
                <div class="dropbox">
                  <input
                    type="file"
                    name="file"
                    class="input-file"
                    :disabled="isUploading"
                    :accept="selectedFileType.fileTypeCode"
                    @change="onSelectFile"
                  />
                  <v-row
                    v-if="isUploading"
                    justify="center"
                  >
                    <v-col cols="8">
                      <v-progress-linear
                        color="primary"
                        rounded
                        :value="uploadedPercentage"
                      />
                    </v-col>
                  </v-row>

                  <v-row
                    justify="center"
                  >
                    <h6
                      v-if="isUploading"
                      class="display-1 mb-1 grey--text"
                    >
                      Uploading {{ selectedFile.name }} file...
                    </h6>

                    <h6
                      v-if="isFileSelected"
                      class="display-1 mb-1 grey--text"
                    >
                      Selected File: {{ selectedFile.name }}
                    </h6>

                    <h6
                      v-if="isInitial"
                      class="display-1 mb-1 grey--text"
                    >
                      Drag your file here to select<br> or click to browse
                    </h6>
                  </v-row>
                </div>
              </v-col>

              <v-col
                cols="12"
                class="text-right"
              >
                <v-btn
                  color="primary"
                  outlined
                  @click="onUploadFile"
                >
                  Upload File
                </v-btn>
              </v-col>
            </v-row>
          </v-container>
        </v-form>
      </base-material-card>
    </v-col>
  </v-row>
</template>

<script>
  import * as axios from 'axios'
  import { BASE_URL } from '../constants.js'

  const STATUS_INITIAL = 0
  const STATUS_FILE_SELECTED = 1
  const STATUS_UPLOADING = 2
  const STATUS_SUCCESS = 3
  const STATUS_FAILED = 4

  export default {
    name: 'Upload',

    data: () => ({
      fileTypes: [
        {
          fileType: 'Video',
          fileTypeCode: 'video/*',
        },
        {
          fileType: 'Music',
          fileTypeCode: 'audio/*',
        },
        {
          fileType: 'Image',
          fileTypeCode: 'image/*',
        },
      ],
      selectedFileType: null,

      // Update Video Genre
      videoGenre: [
        'Electronic music',
        'Funk',
        'Hip hop music',
        'Jazz',
        'Pop music',
        'Rock music',
        'Metal music',
        'Soul music and R&B',
        'Religious music',
        'Traditional and folk music',
      ],
      selectedVideoGenre: null,

      musicGenre: [
        'Electronic music',
        'Funk',
        'Hip hop music',
        'Jazz',
        'Pop music',
        'Rock music',
        'Metal music',
        'Soul music and R&B',
        'Religious music',
        'Traditional and folk music',
      ],
      selectedMusicGenre: null,

      fileLanguage: [
        'Hindi',
        'English',
        'Tamil',
        'Telugu',
      ],
      selectedFileLanguage: null,

      videoTypes: [
        'Movie',
        'TV Series',
        'Music Video',
      ],
      selectedVideoType: null,

      fileTitle: null,
      currentStatus: null,
      selectedFile: null,
      status: {},
      uploadedPercentage: 0,
    }),

    computed: {
      isInitial () {
        return this.currentStatus === STATUS_INITIAL
      },
      isFileSelected () {
        return this.currentStatus === STATUS_FILE_SELECTED
      },
      isUploading () {
        return this.currentStatus === STATUS_UPLOADING
      },
      isSuccess () {
        return this.currentStatus === STATUS_SUCCESS
      },
      isFailed () {
        return this.currentStatus === STATUS_FAILED
      },
    },

    mounted () {
      this.reset()
    },

    methods: {
      onSelectFile (event) {
        if (event.target.files.length !== 0) {
          this.currentStatus = STATUS_FILE_SELECTED
          this.selectedFile = event.target.files[0]
        }
      },

      onUploadFile () {
        const formData = new FormData()

        // If the no file is selected dont upload the file
        // TODO: Validate all the checks in a function and then only enable the upload button else disable it

        if (!this.selectedFile) return

        // append the files to FormData

        formData.append('file', this.selectedFile)

        var self = this
        const url = `${BASE_URL}/FileUpload`
        axios.post(
          url,
          formData,
          {
            headers: {
              'Content-Type': 'multipart/form-data',
            },
            onUploadProgress: uploadEvent => {
              self.currentStatus = STATUS_UPLOADING
              self.uploadedPercentage = parseInt(Math.round((uploadEvent.loaded / uploadEvent.total) * 100))
            },
          },
        )
          .then(res => {
            console.log(res)
            this.currentStatus = STATUS_SUCCESS
            this.status.color = 'success'
            this.status.message = res.message
            this.status.state = res.status
            this.reset()

            // this.status.display = true
            // this.status.isSuccess = res.status
            // this.status.successMessage = res.message
            // console.log(res)
            // , {
            //           onUploadProgress: uploadEvent => {
            //             console.log('Uploading Progress: ' + Math.round(uploadEvent.loaded / uploadEvent.total * 100) + '%')
            //           },
            //         }

            // {
            //   headers: {
            //     'Content-Type': 'multipart/form-data',
            //   },
            // },
            // this.onUploadProgress,

            // lastModified: 1635671934810
            // lastModifiedDate: Sun Oct 31 2021 14:48:54 GMT+0530 (India Standard Time) {}
            // name: "5-cm.mp4"
            // size: 37922050
            // type: "video/mp4"

            // , {
            //     headers: {
            //       'Content-Type': 'multipart/form-data'
            //     }
            //   }

            // this.fileTitle
            // this.selectedFileType
            // this.selectedVideoGenre
            // this.selectedVideoType
            // this.selectedMusicGenre
            // this.selectedFileLanguage
          })
          .catch(error => {
            this.currentStatus = STATUS_FAILED
            this.status.color = 'error'
            this.status.message = error.message
            this.status.state = error.status
          })

        this.$store.commit('SET_NOTIFICATION', this.status)
      },

      reset () {
        // reset form to initial state
        this.currentStatus = STATUS_INITIAL
        this.selectedFileType = this.fileTypes[0]
      },
    },
  }
</script>

<style>
  .dropbox {
    color: dimgray;
    padding: 0px;
    min-height: 200px; /* minimum height */
    position: relative;
    cursor: pointer;
    border: 1px solid rgb(80, 80, 80); /* the dash box */
    border-radius: 5px;
  }

  .input-file {
    opacity: 0; /* invisible but it's there! */
    width: 100%;
    height: 195px;
    position: absolute;
    cursor: pointer;
  }

  .dropbox:hover {
    border-color: primary; /* when mouse over to the drop zone, change color, setup primary color for border
    12px/16px Menlo,Consolas,Monaco,Lucida Console,Liberation Mono,DejaVu Sans Mono,Bitstream Vera Sans Mono,Courier New,monospace,serif*/
  }

  .dropbox h6 {
    text-align: center;
    padding: 75px 0;
  }
</style>
