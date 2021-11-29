# Web Project Boilerplaite

The project contains the following techologies:

- Node
- Webpack
- SCSS
- CSS Extract
- TailwindCSS
- TypeScript
- React
- yarn
- ASP NET Core 6.0

## Prerequirements

- Node v14.18.1
- npm 6.14.15
- .NET 6 SDK
- mkcert

## Development

### Init DEV environment

Run the following commands:

```bash
npm install --global yarn
yarn install
```

### Generate certificates

```bash
choco install mkcert
mkcert -install
```

Generate developer certificate:

```bash
mkcert -key-file test.dev.key -cert-file test.dev.crt test.dev
```

### Add the following line to the `hosts` file

`127.0.0.1       test.dev`

### Run the project

To run the ASP.Net Core hosting application:

```bash
yarn dotnet
```

or to watch the hosting project:

```bash
yarn dotnet:watch
```

To run the Webpack dev server:

```bash
yarn start
```

### How to publish

```bash
yarn publish:site
```

This command will create a publish folder in the root folder of the project.
