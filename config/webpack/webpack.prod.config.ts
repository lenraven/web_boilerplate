import * as path from "./path"
import merge from "webpack-merge";
import commonConfig from "./webpack.common.config";
import webpack from "webpack";
import OptimizeCssAssetsPlugin from "optimize-css-assets-webpack-plugin";
import MiniCssExtractPlugin from "mini-css-extract-plugin";
import {WebpackManifestPlugin} from "webpack-manifest-plugin";

const config: webpack.Configuration = merge(commonConfig, {
  mode: "production",
  entry: {
    main: ["./index.tsx"],
    vendor: ["react", "react-dom"]
  },
  output: {
    path: path.build,
    filename: "js/[name].[fullhash].min.js",
    publicPath: path.PUBLIC_PATH,
    pathinfo: true,
  },
  devtool: "source-map",
  plugins: [
    new OptimizeCssAssetsPlugin({
        cssProcessorOptions: { discardComments: { removeAll: true } },
    }),
    new MiniCssExtractPlugin({
        filename: "css/[name]_[chunkhash].css",
    }),
    new WebpackManifestPlugin({ fileName: "manifest-rev.json" }),
  ],
});

export default config;