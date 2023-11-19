# MultipleBlazorWasm

Project is a template for hosting multiple Blazor WASM.

Try to use endpoints:
- https://localhost:5001/api
- https://localhost:5001/firstapp
- https://localhost:5001/secondapp

Steps:
- add to Blazor wasm csproj StaticWebAssetBasePath with name.
- update in firstapp index.html base href="/firstapp/" and in secondapp index.html base href="/secondapp/"
- add Map statement in server project
