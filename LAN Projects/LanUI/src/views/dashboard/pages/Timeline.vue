<template>
  <v-container
    id="timeline"
    fluid
    tag="section"
  >
    <v-row>
      <v-col>
        <v-timeline align-top>
          <v-timeline-item
            v-for="(timeline, i) in timelines"
            :key="i"
            :color="timeline.color"
            :icon="timeline.icon"
            fill-dot
            large
          >
            <v-card class="pa-6">
              <v-chip
                :color="timeline.color"
                class="overline mb-3"
                small
              >
                {{ timeline.chip }}
              </v-chip>

              <p
                class="subtitle-1 font-weight-light"
                v-text="timeline.text"
              />

              <div
                class="text-uppercase body-2"
                v-text="timeline.subtext"
              />

              <template v-if="timeline.action">
                <v-divider class="mb-3" />

                <v-menu
                  v-model="menu"
                  bottom
                  offset-y
                  origin="top left"
                  right
                  transition="scale-transition"
                >
                  <template v-slot:activator="{ attrs, on }">
                    <v-btn
                      v-bind="attrs"
                      :color="timeline.action"
                      large
                      rounded
                      v-on="on"
                    >
                      <v-icon
                        left
                        v-text="timeline.actionIcon"
                      />
                      <v-icon right>
                        {{ menu ? 'mdi-menu-up' : 'mdi-menu-down' }}
                      </v-icon>
                    </v-btn>
                  </template>

                  <v-sheet>
                    <v-list>
                      <v-list-item
                        v-for="a in timeline.actions"
                        :key="a"
                        link
                      >
                        <v-list-item-title v-text="a" />
                      </v-list-item>
                    </v-list>
                  </v-sheet>
                </v-menu>
              </template>
            </v-card>
          </v-timeline-item>
        </v-timeline>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
  export default {
    name: 'DashboardPagesTimeline',

    data: () => ({
      menu: false,
      timelines: [
        {
          chip: 'Notify Users for New Item',
          color: 'info',
          icon: 'mdi-airplane-landing',
          text: 'Use the signalR server hub to notify everyone if there is a change in the server. Users would be notified if someone uploads a new item into the system.',
        },
        {
          chip: 'Save Theme into the DB',
          color: 'success',
          icon: 'mdi-puzzle',
          text: 'Themes and background should be saved in the database for the user',
        },
        {
          chip: 'Search',
          color: 'warning',
          icon: 'mdi-magnify',
          text: 'User can search data from the search box and show on the search result page',
        },
        {
          chip: 'Installer',
          color: 'error',
          icon: 'mdi-fingerprint',
          text: 'Create an installer which will install and place all the related file at respective places. And create the service/site and start them.',
        },
        {
          chip: 'Login',
          color: 'info',
          icon: 'mdi-login',
          text: 'Login functionality for all the user and save the user data into the DB.',
        },
      ],
    }),
  }
</script>
