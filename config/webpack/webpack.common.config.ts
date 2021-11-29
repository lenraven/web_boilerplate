import webpack from "webpack";
import * as path from "./path";
import MiniCssExtractPlugin from "mini-css-extract-plugin";

const config: webpack.Configuration = {
    resolve: {
        extensions: [".ts", ".tsx", ".js"],
        modules: ["node_modules"],
        alias: {
            components: path.aliases.Components
        },
    },
    context: path.src,
    module: {
        rules: [
            {
                test: /\.ts(x?)$/,
                use: "ts-loader",
                exclude: /node_modules/,
            },
            {
                test: /\.css$/,
                use: [
                  {loader: MiniCssExtractPlugin.loader },                
                  { loader: "css-loader", options: { importLoaders: 1 } },
                ],
              },
            {
              test: /\.(scss|sass)$/,
              use: [
                {loader: MiniCssExtractPlugin.loader },                
                { loader: "css-loader", options: { importLoaders: 1 } },
                { loader: "postcss-loader", options: { postcssOptions: { config: path.config } } },
                { loader: "sass-loader" },
              ],
            },
            {
                test: /\.(jpe?g|png|gif|svg)$/i,
                use: [
                    "file-loader?hash=sha512&digest=hex&name=img/[hash].[ext]",
                    "image-webpack-loader?bypassOnDebug&optipng.optimizationLevel=7&gifsicle.interlaced=false",
                ],
            }
        ]
    },
    performance: {
        hints: false,
    },    
    stats: {
        errorDetails: true
    },
};

export default config;