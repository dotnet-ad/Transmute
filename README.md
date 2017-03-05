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

### Array conversions

If the source type and target type are arrays, and a converter has been registered to convert a source item to a target item, a converter will be available too :

```csharp
var source = new[] { 10, 11, 12 };
var target = transmuter.Convert<string[]>(source); // { "10", "11", "12" }
```

### Default converters

**All platforms**

| From          | To            | Description   |
| ------------- |---------------|---------------|
| short         | byte[]        | BitConverter   |
| int         | byte[]        | BitConverter   |
| long         | byte[]        | BitConverter   |
| float         | byte[]        | BitConverter   |
| double         | byte[]        | BitConverter   |
| bool         | byte[]        | BitConverter   |
| byte[]         | short        | BitConverter   |
| byte[]         | int        | BitConverter   |
| byte[]         | long        | BitConverter   |
| byte[]         | float        | BitConverter   |
| byte[]         | double        | BitConverter   |
| byte[]         | bool        | BitConverter   |
| int         | short        | Convert.ChangeType   |
| short         | int        | Convert.ChangeType   |
| int         | long        | Convert.ChangeType   |
| long         | int        | Convert.ChangeType   |
| int         | float        | Convert.ChangeType   |
| float         | int        | Convert.ChangeType   |
| int         | double        | Convert.ChangeType   |
| double         | int        | Convert.ChangeType   |
| float         | double        | Convert.ChangeType   |
| double         | float        | Convert.ChangeType   |
| int         | bool        | > 0 ?    |
| bool         | int        | true ? 1 : 0    |
| short         | string        | ToString   |
| int         | string        | ToString   |
| long         | string        | ToString   |
| float         | string        | ToString   |
| double         | string        | ToString   |
| bool         | string        | ToString   |
| long         | DateTime        | from timestamp (ms)   |
| DateTime         | long        | to timestamp (ms)   |

**Xamarin.iOS**

| From          | To            | Description   |
| ------------- |---------------|---------------|
| DateTime         | NSDate        | conversion   |
| NSDate         | DateTime        | conversion   |
| CGRect         | int[]        | { x,y,w,h }   |
| int[]         | CGRect        | { x,y,w,h }   |
| CGRect         | float[]        | { x,y,w,h }   |
| float[]         | CGRect        | { x,y,w,h }   |
| int         | UIColor        | 0xAARRGGBB   |
| UIColor         | int        | 0xAARRGGBB   |
| bytes[]         | UIColor        | AA,RR,GG,BB   |
| UIColor         | bytes[]        | AA,RR,GG,BB   |
| bool         | UIColor        | true ? UIColor.Green : UIColor.Red   |
| UIColor         | bool        | value == UIColor.Green   |
| string         | UIImage        | NSData.FromFile -> UIImage.LoadFromData   |

**Xamarin.Android**

| From          | To            | Description   |
| ------------- |---------------|---------------|
| bool         | ViewStates        | true ? ViewStates.Visible : ViewStates.Gone   |
| ViewStates         | bool        | value == ViewStates.Visible   |
| int         | Color        | 0xAARRGGBB   |
| Color         | int        | 0xAARRGGBB   |
| bytes[]         | Color        | AA,RR,GG,BB   |
| Color         | bytes[]        | AA,RR,GG,BB   |
| string         | Bitmap        | BitmapFactory.DecodeFile   |


## Roadmap / Ideas

* Add collection conversion
* Manage conversion errors

### Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

### License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)
