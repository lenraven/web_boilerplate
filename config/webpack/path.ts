import path from "path";

export const src = path.resolve(__dirname, "../../client");
export const build = path.resolve(__dirname, "../../wwwroot");

export const aliases = {
    Components: path.resolve(__dirname, "..", "..", "client", "components")
}

export const config = path.resolve(__dirname, "../");

export const PUBLIC_PATH = "/";
