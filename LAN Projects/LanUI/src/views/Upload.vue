<template>
  <v-container
    fluid
  >
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
                <v-col
                  cols="12"
                  sm="6"
                >
                  <v-select
                    ref="fileType"
                    v-model="selectedFileType"
                    :items="fileTypes"
                    item-text="fileType"
                    item-value="fileTypeCode"
                    label="File Type"
                    required
                    outlined
                    return-object
                    prepend-inner-icon="mdi-file-compare"
                    :error-messages="errorMessages"
                  />
                </v-col>

                <v-col
                  cols="12"
                  sm="6"
                >
                  <v-text-field
                    ref="fileTitle"
                    v-model="fileTitle"
                    label="File Title"
                    outlined
                    required
                    prepend-inner-icon="mdi-file-send"
                    :rules="rules"
                    :error-messages="errorMessages"
                  />
                </v-col>

                <v-col
                  cols="12"
                >
                  <div class="dropbox">
                    <input
                      ref="file"
                      type="file"
                      name="file"
                      class="input-file"
                      :disabled="isUploading"
                      :accept="selectedFileType.fileTypeCode"
                      @change="onSelectFile"
                    >
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

                <v-divider />

                <v-col
                  cols="12"
                  class="text-right"
                >
                  <v-btn
                    color="primary"
                    outlined
                    :disabled="!fileTitle"
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
  </v-container>
</template>

<script>
  import * as axios from 'axios'
  import { variables } from '../constants.js'

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
          fileType: 'Audio',
          fileTypeCode: 'audio/*',
        },
        {
          fileType: 'Image',
          fileTypeCode: 'image/*',
        },
      ],
      selectedFileType: null,
      rules: [
        value => value.length <= 30 || 'Max 30 characters',
        value => !!value || 'Required.',
        value => (value && value.length >= 10) || 'Min 10 characters',
        value => /^[\w\- ]+$/.test(value) || 'No special characters (! @ # $ % ^ & * ( ) { } [ ] . , / \\ ` ~) are allowed',
      ],

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
          this.fileTitle = this.selectedFile.name
        }
      },

      onUploadFile () {
        const formData = new FormData()

        // If the no file is selected dont upload the file
        // TODO: Validate all the checks in a function and then only enable the upload button else disable it

        if (!this.selectedFile || !this.fileTitle || !(this.$refs.fileTitle.validate(true))) return

        // append the files to FormData

        formData.append('file', this.selectedFile)
        formData.append('name', this.fileTitle)
        formData.append('type', this.selectedFileType.fileType)

        var self = this
        const url = `${variables.BASE_URL}/FileUpload`
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
            this.currentStatus = STATUS_SUCCESS
            this.status.color = 'success'
            this.status.message = res.message
            this.status.state = res.status
            this.reset()
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
        this.fileTitle = ''
        this.$refs.fileTitle.reset()
        this.selectedFile = null
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
