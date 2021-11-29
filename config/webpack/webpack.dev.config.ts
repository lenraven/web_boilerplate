import * as path from "./path";
import commonConfig from "./webpack.common.config";
import webpack from "webpack";
import merge from "webpack-merge";
import 'webpack-dev-server';
import * as http from "http";
import * as fs from "fs";
import MiniCssExtractPlugin from "mini-css-extract-plugin";

const config: webpack.Configuration = merge(commonConfig, {
    devtool: "eval-source-map",
    mode: "development",
    entry: {
        main: ["./index.tsx"],
        vendor: ["react", "react-dom"]
    },
    output: {
        path: path.build,
        filename: "js/[name].js",
        publicPath: path.PUBLIC_PATH,
        pathinfo: true,
    },
    plugins: [
		new MiniCssExtractPlugin({
			filename: "css/[name].css",
		}),
    ],
    devServer: {
        open: false,
        headers: { "Access-Control-Allow-Origin": "*" },
        proxy: [
            {
                context: (_pathname: string, _req: http.IncomingMessage) => {
                    return true;
                },
                target: "http://localhost:5554",
                secure: false,
            }
        ],
        host: "test.dev",
        port: 4443,
        server: {
            type: "https",
            options: {
                cert: fs.readFileSync("./test.dev.crt", "utf-8"),
                key: fs.readFileSync("./test.dev.key", "utf-8")
            }
        },
        hot: true
    }
});

export default config;
