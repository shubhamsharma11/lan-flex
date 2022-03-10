import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

export default new Router({
  routes: [
        // Dashboard
        {
          name: 'Dashboard',
          path: '/',
          component: () => import('@/views/dashboard/Dashboard'),
        },
        // Videos
        {
          name: 'Videos',
          path: '/videos',
          component: () => import('@/views/Videos'),
        },
        // Audios
        {
          name: 'Audios',
          path: '/audios',
          component: () => import('@/views/Audios'),
        },
        // Images
        {
          name: 'Images',
          path: '/images',
          component: () => import('@/views/Images'),
        },
        // Upload
        {
          name: 'Upload',
          path: '/upload',
          component: () => import('@/views/Upload'),
        },
        // History
        {
          name: 'History',
          path: '/history',
          component: () => import('@/views/History'),
        },
        // Features
        {
          name: 'Features',
          path: '/features',
          component: () => import('@/views/Features'),
        },
        // Pages
        {
          name: 'User Profile',
          path: '/pages/user',
          component: () => import('@/views/dashboard/pages/UserProfile'),
        },
        {
          name: 'Notifications',
          path: '/components/notifications',
          component: () => import('@/views/dashboard/component/Notifications'),
        },
        {
          name: 'Icons',
          path: '/components/icons',
          component: () => import('@/views/dashboard/component/Icons'),
        },
        {
          name: 'Typography',
          path: '/components/typography',
          component: () => import('@/views/dashboard/component/Typography'),
        },
        // Maps
        {
          name: 'Google Maps',
          path: '/maps/google-maps',
          component: () => import('@/views/dashboard/maps/GoogleMaps'),
        },
  ],
})
