<template>
  <v-container
    id="regular-tables"
    fluid
    tag="section"
  >
    <base-material-tabs
      v-model="tab"
      background-color="transparent"
      centered
      color="primary"
      icons-and-text
    >
      <v-tab
        v-for="(tab, i) in tabs"
        :key="i"
      >
        {{ tab.text }}
        <v-icon v-text="tab.icon" />
      </v-tab>

      <v-tabs-items
        v-model="tab"
        class="pt-5 transparent"
      >
        <v-tab-item>
          <v-card class="mt-0 px-5">
            <v-card-title>
              Task History
              <v-spacer></v-spacer>
              <v-text-field
                v-model="search"
                append-icon="mdi-magnify"
                label="Search"
                single-line
                hide-details
              ></v-text-field>
            </v-card-title>
            <v-card-text>
            <v-data-table
              :headers="headers"
              :items="items"
              :search="search"
              :loading="loading"
              loading-text="Loading... Please wait"
            >
            </v-data-table>
            </v-card-text>
          </v-card>
        </v-tab-item>
      </v-tabs-items>
    </base-material-tabs>
  </v-container>
</template>
<script>
  import * as axios from 'axios'
  import { variables } from '../constants.js'

  export default {
    name: 'History',

    data: () => ({
      tab: 0,
      tabs: [
        {
          text: 'Task History',
          icon: 'mdi-history',
        },
      ],
      search: '',
      loading: false,
      headers: [
        { text: 'File Name', value: 'Name' },
        { text: 'File Type', value: 'Type' },
        { text: 'Uploaded Time', value: 'UploadedTime' },
      ],
      items: [],
    }),

    mounted () {
      this.getHistory()
    },

    methods: {
      getHistory () {
        this.loading = true
        axios.get(`${variables.BASE_URL}/GetHistory`)
          .then(response => {
            this.items = response.data
            this.loading = false
          })
      },
      getColor (calories) {
        if (calories > 400) return 'red'
        else if (calories > 200) return 'orange'
        else return 'green'
      },

    },
  }
</script>
