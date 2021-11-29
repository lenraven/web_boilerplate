import * as React from "react";
import { render } from "react-dom";
import { App } from "./components/app";
import "./tailwind.scss";

const container = document.getElementById("container");

render(<App content="Hello, World!" />, container);