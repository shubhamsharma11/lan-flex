module.exports = {
  devServer: {
    // proxy: 'http://localhost:49622/UploadedFiles/',
    proxy: 'http://192.168.104.56:49622/UploadedFiles/',
  },

  // devServer: {
  //   proxy: 'http://localhost:49622/UploadedFiles'
    
  //   //http://localhost:60749/api/
    
  //   // {
  //   // '/data': {
  //   //   target: 'http://localhost:49622/',
  //   //   changeOrigin: true,
  //   //   secure: false,
  //   //   pathRewrite: {'^/data': '/data'},
  //   //   logLevel: 'debug'
  //   // },
  //   // '/UploadedFiles': {
  //   //   target: 'http://localhost:49622/UploadedFiles',
  //   //   changeOrigin: true,
  //   //   secure: false,
  //   //   logLevel: 'debug' 
  //   // },
  //   // }
  // },

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
