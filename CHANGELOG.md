## [1.23.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.23.0...1.23.1) (2021-02-01)


### Bug Fixes

* Fixed "type 'MonoScript' is defined in a not referenced assembly" in Unity 2019 ([8d2b747](https://github.com/SolidAlloy/SolidUtilities/commit/8d2b747e2a25dd6700d0b87ca14fb50d9f4d54fb))

# [1.23.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.22.1...1.23.0) (2021-01-30)


### Bug Fixes

* Fixed the class regex to find generic classes even if they derive or implement an interface ([87e3197](https://github.com/SolidAlloy/SolidUtilities/commit/87e319744197a5122cb767315b3bf51342bbc515))
* Started handling assembly-loading exceptions in MonoScript.GetClassType() ([eb999ac](https://github.com/SolidAlloy/SolidUtilities/commit/eb999ac69800d4a8e1ee066983bffcd682698c54))


### Features

* Added ArrayEqualityComparer - a generic EqualityComparer for arrays ([732130d](https://github.com/SolidAlloy/SolidUtilities/commit/732130dd09e775a97e673e34841e548a186846b8))
* Added AssetDatabaseHelper.GetTypeFromGUID() method ([f3317ed](https://github.com/SolidAlloy/SolidUtilities/commit/f3317ed500ea97b1d971941cec1ebd1ea15932b0))
* Added AssetSearcher.GetAssetDetails and GetMonoScriptFromType methods ([d18e9fb](https://github.com/SolidAlloy/SolidUtilities/commit/d18e9fb4e7f03524f03f92109647914f36151930))
* Added AssetSearcher.GetClassGUID(Type) method ([593861a](https://github.com/SolidAlloy/SolidUtilities/commit/593861a8d232696245fd7909e87716bf74260b45))
* Added DLLs with access to Unity internal types and methods ([f382686](https://github.com/SolidAlloy/SolidUtilities/commit/f38268667d528b7dd1bfd8c3aa7158af6666dd17))
* Added EditorGUIUtilityProxy.GetMainWindowPosition() method for Unity under 2020.1 ([75213b7](https://github.com/SolidAlloy/SolidUtilities/commit/75213b78a59d643cceea2c6f9856251564e99d93))
* Added enum.ContainsFlag() method ([514456c](https://github.com/SolidAlloy/SolidUtilities/commit/514456c3abd768631231836f0395f6ee087b199a))
* Added FastIterationDictionary that allows for faster iteration over it at a cost of larger memory footprint ([f41f89c](https://github.com/SolidAlloy/SolidUtilities/commit/f41f89c2887d88c3e0588c5c0ede04e24f6774e2))
* Added HashSet.SetEqualsArray and HashSet.SetEqualsList methods ([2337b49](https://github.com/SolidAlloy/SolidUtilities/commit/2337b49e628b383c88b8c1d970e8679a803e4ac0))
* Added HashSet<T>.ExceptWithAndCreateNew() and IntersectWithAndCreateNew() methods ([b287d0e](https://github.com/SolidAlloy/SolidUtilities/commit/b287d0e6a2e680df108b3cb297e1eda7715bbc80))
* Added LogHelper.Clear() and LogHelper.GetCountByType() methods ([e6fa771](https://github.com/SolidAlloy/SolidUtilities/commit/e6fa77164422450505bdcfbeeed53e561b74d40b))
* Added LogHelper.GetCount() method that returns the total number of log entries in the console ([deda0d5](https://github.com/SolidAlloy/SolidUtilities/commit/deda0d5af463c9e961bc2d4124d4817bd6f1eaee))
* Added string.CountSubstrings() extension method ([37ba570](https://github.com/SolidAlloy/SolidUtilities/commit/37ba570344fd6d07acbaafa6f9d20c3247c094ec))
* Added string.GetSubstringAfterLast(char) method ([6722b57](https://github.com/SolidAlloy/SolidUtilities/commit/6722b575e5cc9b17d4f2a9ea2f60ba93b89e1ae9))
* Added string.ReplaceWithBuiltInName() extension method for type names. ([a89fbbf](https://github.com/SolidAlloy/SolidUtilities/commit/a89fbbf936081ecdd3cdc84a41b39940b9f04ad8))
* Added the LogHelper.RemoveLogEntriesByMode() method ([8af2a23](https://github.com/SolidAlloy/SolidUtilities/commit/8af2a23b4cf0090dbb11c33e2a7191cc3bb24bb2))
* Added Type.GetShortAssemblyName method ([356bdca](https://github.com/SolidAlloy/SolidUtilities/commit/356bdca8e29d90d11be20fc8a39a23c4a7fae626))
* Added Type.HasAttribute<T>() method ([3789b29](https://github.com/SolidAlloy/SolidUtilities/commit/3789b296d20cb5d286157a2c0077974a8b8fbb68))
* AssetSearcher.GetClassGUID() can now find GUID of a generic type even if the script does not have generic suffix in its name ([e1ce428](https://github.com/SolidAlloy/SolidUtilities/commit/e1ce428c3393fcbbe4631c3158b49cdfb2d3ad0a))
* Made MonoScript.GetClassType() work with custom non-generic classes ([842c17a](https://github.com/SolidAlloy/SolidUtilities/commit/842c17a3043212c1e299e6408bc418e739d8dbb5))
* Made StripGenericSuffix work only for the type names that contain backlash ([1c6add8](https://github.com/SolidAlloy/SolidUtilities/commit/1c6add8a44d20a224b47cd473b4f31c27bee1725))
* Moved HasAttribute<T>() from Type extensions to MemberInfo extensions ([d730d34](https://github.com/SolidAlloy/SolidUtilities/commit/d730d34a0ea2bb8b52afd817c8652be75de2b190))
* Removed AssetCreator class ([8a874e2](https://github.com/SolidAlloy/SolidUtilities/commit/8a874e2119d94d8e8db6dbcb9db00dfea4ce66a9))
* Removed IEnumerable.ForEach method ([2f8ec6e](https://github.com/SolidAlloy/SolidUtilities/commit/2f8ec6e0c426de3b915fe965a4c6d25443c0aa4b))
* Replaced EditorDrawHelper DrawInScrollView and DrawVertically methods with ScrollView and VerticalBlock structs ([0aaaf3e](https://github.com/SolidAlloy/SolidUtilities/commit/0aaaf3e8d2d94ea12c1eda7b6f5ca854bd84b6df))


### Performance Improvements

* Got rid of some unnecessary memory allocations ([a6b6464](https://github.com/SolidAlloy/SolidUtilities/commit/a6b6464281e754922b7a0e9f94258cf16e8e4ff4))
* Improved performance of AssetSearcher.GetClassGUID() ([27c8f57](https://github.com/SolidAlloy/SolidUtilities/commit/27c8f57a9bae6bf72f004fb90a46bd6fc2ad7056))
* Made class regex a compiled static field member. ([f8f2eeb](https://github.com/SolidAlloy/SolidUtilities/commit/f8f2eeb8bb612951c8dcf051eacba80704b00a83))
* Removed an accidental NUnit using statement ([62a6640](https://github.com/SolidAlloy/SolidUtilities/commit/62a6640cb2d3f3cbfd0ec19a9f1178659a6808fe))
* Started using Internals DLLs instead of reflection to access Unity internal members ([f44732f](https://github.com/SolidAlloy/SolidUtilities/commit/f44732f072add5890ba2d783002434581afbfe86))

## [1.22.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.22.0...1.22.1) (2020-12-17)


### Bug Fixes

* Fixed NullReferenceException when opening a drop-down with no types available ([9f9628b](https://github.com/SolidAlloy/SolidUtilities/commit/9f9628b3c55f059913bb9a3edc66a9dec14a17e1))

# [1.22.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.21.1...1.22.0) (2020-12-10)


### Features

* Added string.MakeClassFriendly() and string.StripGenericSuffix() methods ([e8a7036](https://github.com/SolidAlloy/SolidUtilities/commit/e8a70362899c566c0de76336d8f27e15aceb97eb))

## [1.21.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.21.0...1.21.1) (2020-12-04)


### Bug Fixes

* Fixed assertion error when generic object field is used in a custom serialized class ([e4894bb](https://github.com/SolidAlloy/SolidUtilities/commit/e4894bb4579fcbb2f835a19b88f18f6c3eb66f4c))

# [1.21.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.20.2...1.21.0) (2020-12-04)


### Bug Fixes

* Fixed assertion error when private GenericSO field is used in the parent class of a MonoBehaviour ([f32fd7b](https://github.com/SolidAlloy/SolidUtilities/commit/f32fd7bc83c820b5a4fcffa5267a25f1fde3e8ef))


### Features

* Added the Type.GetFieldRecursive method to search for a field in the whole class hierarchy ([f956fec](https://github.com/SolidAlloy/SolidUtilities/commit/f956fecec6ed3036d78647d799fa9d4e0cc2fd70))

## [1.20.2](https://github.com/SolidAlloy/SolidUtilities/compare/1.20.1...1.20.2) (2020-12-03)


### Bug Fixes

* Fixed the ArgumentException when using ResizableTextArea attribute ([4263516](https://github.com/SolidAlloy/SolidUtilities/commit/4263516074e3fe17f98213f00bb61457131a8d5a))

## [1.20.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.20.0...1.20.1) (2020-12-02)


### Bug Fixes

* Started initializing GUIStyles in properties instead of field initializers ([a77f9f6](https://github.com/SolidAlloy/SolidUtilities/commit/a77f9f69198f5c25ccd22d33eb46b12c8a115e9e))

# [1.20.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.19.0...1.20.0) (2020-12-02)


### Features

* Added the Vector2.RoundUp() method ([c57a9fa](https://github.com/SolidAlloy/SolidUtilities/commit/c57a9fa85df37770b1fec78b2f4fad47c61d5af0))

# [1.19.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.18.3...1.19.0) (2020-12-02)


### Features

* Added AssetSearcher.FindObjectsWithValue() that searches for assets and scene objects that contains a variable with a certain value. ([33a3872](https://github.com/SolidAlloy/SolidUtilities/commit/33a38721e849557db318eb422f29e9efd6473237))
* Added RegexExtensions class with its first method - Find() ([d2b501e](https://github.com/SolidAlloy/SolidUtilities/commit/d2b501e039c9d0a95f9c206bf388b53949dc41e3))
* Added RelativeToAbsolutePath and GUIDToAbsolutePath methods ([8ad53bd](https://github.com/SolidAlloy/SolidUtilities/commit/8ad53bd3e7c9ab4488dfba0209fdf508f0ffe24f))

## [1.18.3](https://github.com/SolidAlloy/SolidUtilities/compare/1.18.2...1.18.3) (2020-12-01)


### Bug Fixes

* Fixed the missing right border in EditorDrawHelper.DrawBorders() ([4a9a8b8](https://github.com/SolidAlloy/SolidUtilities/commit/4a9a8b8d374cc1b1cf314108e6b3f7ab5493c6a0))

## [1.18.2](https://github.com/SolidAlloy/SolidUtilities/compare/1.18.1...1.18.2) (2020-11-30)


### Bug Fixes

* Moved GetMainWindowPosition to EditorDrawHelper and fixed incorrect preprocessor directives ([2838d6a](https://github.com/SolidAlloy/SolidUtilities/commit/2838d6a9adb6ecbe06c8dc59f67e725f10f1e0ca))
* Moved the GetAllDerivedTypes method to EditorDrawHelper ([cbc143c](https://github.com/SolidAlloy/SolidUtilities/commit/cbc143c838cc2d7830b4164abab8e8d3bc026e51))

## [1.18.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.18.0...1.18.1) (2020-11-29)


### Bug Fixes

* hid all instances EditorGUIUtility.GetMainWindowPosition() under the #if preprocessor directive ([a834cea](https://github.com/SolidAlloy/SolidUtilities/commit/a834ceaab6a510c74c3177b792deb1cbd56f80e1))

# [1.18.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.17.1...1.18.0) (2020-11-27)


### Bug Fixes

* Made SerializedProperty.GetObjectType work with custom serializable classes ([f385e1e](https://github.com/SolidAlloy/SolidUtilities/commit/f385e1eb0917aed59daa6ccfc831df23ef2f33ff))
* Started using a more reliable method to check if EditorWindow is overflowing the screen ([e23abce](https://github.com/SolidAlloy/SolidUtilities/commit/e23abcec0559975907781fd6bcab97895642bb3d))
* Started using correct indent level in DrawWithIndentLevel() ([9b43825](https://github.com/SolidAlloy/SolidUtilities/commit/9b438257c5da6c914c2a6ef7e7c57b1d5a16c475))


### Features

* Added new EditorDrawHelper.DrawWithIndentLevel() method ([cad369c](https://github.com/SolidAlloy/SolidUtilities/commit/cad369c1b2a4964b93eb88f135d0a4591eb46e6a))
* Added ResizableTextArea attribute ([23664a5](https://github.com/SolidAlloy/SolidUtilities/commit/23664a5382d6990ccc5c99aa7d9fa4ca9f455b1e))
* Added the CreateEditor<T>() method ([94b4c02](https://github.com/SolidAlloy/SolidUtilities/commit/94b4c02eb8a21627a77fa1687e659aef2b74d67f))
* Added the GetScreenWidth method to calculate the sum of screen widths if multiple displays are used. ([1c3243b](https://github.com/SolidAlloy/SolidUtilities/commit/1c3243b3c79cc4987b31477caaab147260e7b6fc))

## [1.17.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.17.0...1.17.1) (2020-11-08)


### Bug Fixes

* Replaced triangle_right_16.png to avoid errors for some users ([a78106a](https://github.com/SolidAlloy/SolidUtilities/commit/a78106a6a18d87510922400e19877cc5d1881415))

# [1.17.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.16.0...1.17.0) (2020-11-08)


### Features

* Made ChildProperties iterate over visible properies only by default ([b25f81a](https://github.com/SolidAlloy/SolidUtilities/commit/b25f81a3ac0d6d108d0bc533f456ff23f8f9639c))

# [1.16.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.15.0...1.16.0) (2020-11-07)


### Features

* ChildProperties now does not iterate over built-in properties by default ([5bf6a03](https://github.com/SolidAlloy/SolidUtilities/commit/5bf6a03a01ad78af1cf1c75ae990a02d226605fb))

# [1.15.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.14.0...1.15.0) (2020-11-04)


### Features

* Added IEnumerable.ToHashSet() method ([3422287](https://github.com/SolidAlloy/SolidUtilities/commit/34222872a9cbdf862bc6babf255b33acb6c12109))

# [1.14.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.13.0...1.14.0) (2020-10-29)


### Features

* Made SerializedProperty.GetObjectType work with the collection fields ([d81529b](https://github.com/SolidAlloy/SolidUtilities/commit/d81529b5fab3cc551962e972a0463785e2eb102f))

# [1.13.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.12.0...1.13.0) (2020-10-28)


### Features

* Added SerializedProperty.GetObjectType and EditorDrawHelper.InPropertyWrapper methods ([6d031f7](https://github.com/SolidAlloy/SolidUtilities/commit/6d031f78406b1cb4d6fa4f7ae284444bbbab207d))

# [1.12.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.11.0...1.12.0) (2020-10-27)


### Bug Fixes

* resolved the issue with absent EditorGUIUtility.GetMainWindowPosition on older Unity versions ([d47fc21](https://github.com/SolidAlloy/SolidUtilities/commit/d47fc21b03a6ec5523237b5da54e3566660d9772))


### Features

* Added Hash.SHA1 method ([f7fb534](https://github.com/SolidAlloy/SolidUtilities/commit/f7fb534c197d1bb283258463498a348fd9847b7d))
* Added PackageSearcher.FindPackageByName method ([d028c84](https://github.com/SolidAlloy/SolidUtilities/commit/d028c84c537df1243518f5732abb91a2ce265887))
* Added the DrawHelper.WidthDisabledGUI method ([58e72c2](https://github.com/SolidAlloy/SolidUtilities/commit/58e72c21a8bd2847717043708329b990a325e75b))
* Added the ReadOnly attribute ([7b5f46d](https://github.com/SolidAlloy/SolidUtilities/commit/7b5f46d26208696d7e55bb0c68c39536ad5eb4ea))

# [1.11.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.10.0...1.11.0) (2020-10-22)


### Features

* Added the string.IsValidPath method ([63cef87](https://github.com/SolidAlloy/SolidUtilities/commit/63cef873e6029e08c012816fcffd4e2c559d49fe))

# [1.10.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.9.0...1.10.0) (2020-10-20)


### Features

* Added Type.IsEmpty method ([d9d3978](https://github.com/SolidAlloy/SolidUtilities/commit/d9d3978b7578a162d1a2f98d66c0423c17c6bd13))

# [1.9.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.8.1...1.9.0) (2020-10-20)


### Features

* Added two new extension methods: MonoScript.GetClassType and MonoScript.GetNamespaceName ([c55fc09](https://github.com/SolidAlloy/SolidUtilities/commit/c55fc093bff35868a37e13ebb4844306994c79d9))

## [1.8.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.8.0...1.8.1) (2020-10-18)


### Bug Fixes

* AssetCreator no longer exists rename mode as soon as the asset is created ([406f910](https://github.com/SolidAlloy/SolidUtilities/commit/406f910a30aa45fade385501fd1743ff409e6971))

# [1.8.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.7.1...1.8.0) (2020-10-15)


### Bug Fixes

* Added handling of different rect sizes for Vector2.Center() ([3084c57](https://github.com/SolidAlloy/SolidUtilities/commit/3084c57ccd0d91ac4765cc9483d50ecd4ecc4aa2))


### Features

* Added EditorWindow.CenterOnMainWin method ([3c94dad](https://github.com/SolidAlloy/SolidUtilities/commit/3c94dad795b18431375f7757c880b66beeb24b0a))
* Added Vector2.Center(Rect outerRect) method ([0d23827](https://github.com/SolidAlloy/SolidUtilities/commit/0d238273e512c28e447ae68f544d38691a50a18c))

## [1.7.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.7.0...1.7.1) (2020-10-13)


### Bug Fixes

* Removed static modifiers from local methods for C[#7](https://github.com/SolidAlloy/SolidUtilities/issues/7) compatibility ([35a1496](https://github.com/SolidAlloy/SolidUtilities/commit/35a1496be8ea0923572d66a1699df33bd65dcab6))

# [1.7.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.6.0...1.7.0) (2020-10-12)


### Bug Fixes

* Corrected the IsUnitySerializable method so that it doesn't include serializable classes from the System namespace ([fa83be9](https://github.com/SolidAlloy/SolidUtilities/commit/fa83be955ca93dfc2cadfe0fc58b3ea54f1bf062))


### Features

* Added TypeExtensions.IsUnitySerializable method ([70e1443](https://github.com/SolidAlloy/SolidUtilities/commit/70e14436c8a4dcae714e0954e27d8af5008babc2))

# [1.6.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.5.0...1.6.0) (2020-10-10)


### Features

* Added MakeSureIsGenericTypeDefinition Type extension method. ([2bc1f86](https://github.com/SolidAlloy/SolidUtilities/commit/2bc1f8669a1a9c3867cf123ee282ec9fd73f3a79))

# [1.5.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.4.0...1.5.0) (2020-10-08)


### Features

* Added EditorWindow.MoveOutOfScreen and AssetDatabaseHelper.MakeSureFolderExists ([d5ff96e](https://github.com/SolidAlloy/SolidUtilities/commit/d5ff96e88bd75171c7efddf6c3bd8c5e708484fb))

# [1.4.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.3.0...1.4.0) (2020-10-07)


### Features

* Added AssetCreator, a wrapper of ProjectWindowUtil.CreateAsset() ([7335615](https://github.com/SolidAlloy/SolidUtilities/commit/7335615a015550833e03311a879f70ef1707f813))

# [1.3.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.2.1...1.3.0) (2020-10-06)


### Features

* Added a measure unit choice for Timer.LogTime() ([7d61f96](https://github.com/SolidAlloy/SolidUtilities/commit/7d61f96ca8beafce856cb64f2a571f7f7af69217))

## [1.2.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.2.0...1.2.1) (2020-10-04)


### Bug Fixes

* Made -1 the default value of Resize() instead of 0 ([58f15e6](https://github.com/SolidAlloy/SolidUtilities/commit/58f15e62270543a0f75f9ddf15667b0cc9e8db56))

# [1.2.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.1.0...1.2.0) (2020-10-04)


### Features

* Added new method FloatExtensions.DoesNotEqualApproximately() ([dba7056](https://github.com/SolidAlloy/SolidUtilities/commit/dba7056cc05b042792f38594945a20ae9e4f25a5))

# [1.1.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.0.0...1.1.0) (2020-10-03)


### Features

* Added EditorWindowExtensions and its first method - Resize ([59781b0](https://github.com/SolidAlloy/SolidUtilities/commit/59781b018836262c49c10dcd910e2c31f5f6d350))
