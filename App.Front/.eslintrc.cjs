/* eslint-env node */
require('@rushstack/eslint-patch/modern-module-resolution')

module.exports = {
    devServer: {
        proxy: {
            '^/users': {
                target: 'http://localhost:32770/',
                ws: true,
                changeOrigin: true
            },
        }
    },
  root: true,
  'extends': [
    'plugin:vue/vue3-essential',
    'eslint:recommended',
    '@vue/eslint-config-typescript'
  ],
  parserOptions: {
    ecmaVersion: 'latest'
  }
}
