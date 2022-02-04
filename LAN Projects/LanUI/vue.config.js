module.exports = {
  devServer: {
    proxy: 'http://192.168.43.12:49622/UploadedFiles/',
  },

  transpileDependencies: ['vuetify'],

  pluginOptions: {
    i18n: {
      locale: 'en',
      fallbackLocale: 'en',
      localeDir: 'locales',
      enableInSFC: false,
    },
  },
}
