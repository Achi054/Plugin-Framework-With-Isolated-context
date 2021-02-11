# Plugin Framework with Isolated Context

Project illustrates how to compose a plugin framework .net core as host with .net standard libraries as plugins.

## Problem Statement

Creating plugin framework with isolated assembly load context. As each plugin would refer a different version of `PluginBase`, it would be ideal to have a each plugin running in its own load context.

## Setup

**`PluginApp`** - .net core 3.1 host project, console app<br/>
**`PluginBase`** - .net standard 2.1 plugin contract library<br/>
**`AuditPlugin`** - .net standard 2.1 plugin library<br/>
**`LoginPlugin`** - .net standard 2.1 plugin library<br/>
**`SerializationPlugin`** - .net core 3.1 plugin library

`PluginBase` has 2 versions `2.0.0.0` & `2.2.0.0`<br/>
`AuditPlugin` & `LoginPlugin` references `2.0.0.0`<br/>
`SerializationPlugin` references `2.2.0.0`

## References

[JeremyBytes - Dynamic loading .net standard libraries in .net core](https://jeremybytes.blogspot.com/2020/01/using-typegettype-with-net-core.html)<br/>
[JeremyBytes - Dynamically loading types in .net core with a custom assembly load context](https://jeremybytes.blogspot.com/2020/01/dynamically-loading-types-in-net-core.html)
