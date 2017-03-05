# Transmute

It's very common to convert data from a type to another and there's plenties of APIs to achieve it in .NET. **Transmute** was created to have a single centralized channel for all conversions.

## Install

Available on NuGet

[![NuGet](https://img.shields.io/nuget/v/Transmute.svg?label=NuGet)](https://www.nuget.org/packages/Transmute/)

## Quickstart

```csharp
//.netstandard
var b = Transmuter.Default.Convert<bool>(5); // true
var d = Transmuter.Default.Convert<DateTime>(1495820216); // 26/5/2017

//Xamarin.iOS
var c = Transmuter.Default.Convert<UIColor>(0xFF0000); // red
```

## Functionalities

### Custom converter

You can register a custom converters at any time with `Register` function :

```csharp
Transmuter.Default.Register<T1,T2>(t1 => /* to t2 */);
```

If a converter already exists for those types, it will be replaced by yours.

### Composability

Transmuter will try to find a way to convert a value to a the target type if a path exists.

So if you register those converters :

```csharp
Transmuter.Default.Register<T1,T2>(...);
Transmuter.Default.Register<T2,T3>(...);
Transmuter.Default.Register<T3,T4>(...);
```

This conversions will succeed :

```csharp
T1 t1 = ...;
T4 t4 = Transmuter.Default.Convert<T4>(t1); // T1 -> T2,T2 -> T3,T3 -> T4
```

## Roadmap / Ideas

* Improve performances by sorting converters

### Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

### License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)
