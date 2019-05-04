const path = require('path');
const VueLoaderPlugin = require('vue-loader/lib/plugin');

module.exports = {
  entry: path.resolve(__dirname, 'Scripts', 'inline-editors-bundle.js'),
  output: {
    path: path.resolve(__dirname, 'Content', 'InlineEditors'),
    filename: "inline-editors-bundle.dist.js"
  },
  externals: [{
    vue: 'Vue'
  }],
  module: {
    rules: [{
      test: /\.vue$/,
      loader: "vue-loader",
    }, {
      test: /\.scss$/,
      use: [
        'vue-style-loader',
        'css-loader',
        'sass-loader'
      ]
    }]
  },
  plugins: [
    new VueLoaderPlugin()
  ],
  performance: {
    hints: 'error'
  },
  stats: 'minimal',
  mode: 'development',
  devtool: 'eval-source-map'
};
