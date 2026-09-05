# C# 15 Demos

Small demonstrations of upcoming C# 15 language features.

This repository currently contains a simple discriminated-union demo. More C# 15 examples will be added soon.

## Prerequisites

This project uses preview tooling:

- [.NET 11 SDK (Preview)](https://dotnet.microsoft.com/download/dotnet/11.0)
- [Visual Studio Code Insiders](https://code.visualstudio.com/insiders/)
- [C# Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

Install both the C# Dev Kit and C# extensions in the Insiders edition of Visual Studio Code.

## Run From A Terminal

From the repository directory, restore, build, and run the project:

```powershell
dotnet run
```

To build without running it:

```powershell
dotnet build
```

The project targets `.NET 11` and enables preview language features with `LangVersion` set to `preview` in `HelloDotNetUnions.csproj`.

## Run And Debug In Visual Studio Code

1. Open the repository folder in Visual Studio Code Insiders.
2. Make sure the C# Dev Kit and C# extensions are installed and enabled.
3. Open `Program.cs`.
4. Press `F5` or choose **Run and Debug**.

The included VS Code configuration builds the project first and launches the resulting `.NET 11` application with the debugger attached.

## License

This repository is a collection of learning demos and examples.
