# [1.40.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.39.0...1.40.0) (2022-07-24)


### Bug Fixes

* Fixed exception when passing null array to ArrayHelper.Add() ([38c5068](https://github.com/SolidAlloy/SolidUtilities/commit/38c5068e9df3355211d48f08feb3efcb3522bf9c))
* Improved performance of ListHelper.EmptyList<T>() ([bb218be](https://github.com/SolidAlloy/SolidUtilities/commit/bb218be452264f4ef82d82db65fdbd367cfd7f4d))
* Resolved a GUID conflict with old SerializableDictionary ([a967057](https://github.com/SolidAlloy/SolidUtilities/commit/a967057ec472130101c8ac24f762523eee4aa18c))


### Features

* Added SerializedObject.SetHideFlagsPersistently() extension method ([0965d89](https://github.com/SolidAlloy/SolidUtilities/commit/0965d89bf71c0d6ee97f4c9d1ed31e658b6c0ccf))
* Switched from GUID to assembly names in asmdefs ([67002a9](https://github.com/SolidAlloy/SolidUtilities/commit/67002a9d4b05e616a59bfc9cc826f70a2f07311e))

# [1.39.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.38.2...1.39.0) (2022-06-07)


### Features

* Added an option to exclude assembly from editor during import ([5f7dea7](https://github.com/SolidAlloy/SolidUtilities/commit/5f7dea715a639b2f52badcaeafa81a652d6412bc))

## [1.38.2](https://github.com/SolidAlloy/SolidUtilities/compare/1.38.1...1.38.2) (2022-05-19)


### Bug Fixes

* Fixed the issue with editor icons disappearing randomly ([fadd6a7](https://github.com/SolidAlloy/SolidUtilities/commit/fadd6a7e82d812db1651885370ee0b1aa9d38078))

## [1.38.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.38.0...1.38.1) (2022-05-13)


### Bug Fixes

* Fixed abstract serializable classes not being considered serializable by type.IsUnitySerializable() ([7afc0b2](https://github.com/SolidAlloy/SolidUtilities/commit/7afc0b209383f17725e4c078a1357e8881498198))

# [1.38.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.37.1...1.38.0) (2022-05-13)


### Bug Fixes

* Fixed editor icons missing after exiting play mode ([4280803](https://github.com/SolidAlloy/SolidUtilities/commit/42808039419f891d519142c202bdef907ceb7325))


### Features

* Added Enum.DoesNotContainFlag() extension method ([24f68bd](https://github.com/SolidAlloy/SolidUtilities/commit/24f68bd24e1f685ec790fc2a0959cfbf2f9d9598))

## [1.37.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.37.0...1.37.1) (2022-05-10)


### Bug Fixes

* Fixed incorrect dependency in SolidUtitlites assembly definition ([f8e0bb9](https://github.com/SolidAlloy/SolidUtilities/commit/f8e0bb93a36bfddcf5bef5224b9ecc3c330054b9))

# [1.37.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.36.2...1.37.0) (2022-05-02)


### Bug Fixes

* Fixed errors related to read-only scenes during build ([d8a4220](https://github.com/SolidAlloy/SolidUtilities/commit/d8a4220c0cb01809ade44b285a57c0de9ede4ecd))
* Fixed the prefabs not being found in ProjectWideSearcher ([a916bf1](https://github.com/SolidAlloy/SolidUtilities/commit/a916bf1466dd0b4f392fe65b8747397b0a6e0e55))
* Replaced buttons of plus-i and plus-s with a less blurry ones ([fa7642d](https://github.com/SolidAlloy/SolidUtilities/commit/fa7642df70fdc1039d8da399d56ab4650701a1a9))


### Features

* Added ProjectDependencySearcher that searches for SerializedObjects that will be put in a build, or objects that a scene depends upon, etc. ([f286e4e](https://github.com/SolidAlloy/SolidUtilities/commit/f286e4e674b3598304de5a441ae361df609564a7))
* Added ProjectDependencySearcher.GetSerializedObjectsFromOpenScenes() method ([a54a6ea](https://github.com/SolidAlloy/SolidUtilities/commit/a54a6ea2bc91191a0edb0bae0e8bdbcb8c7e20e2))
* Added ProjectWideSearcher that searches for all SerializedObjects in the project, or in a certain scene, prefab, etc. ([ee3da0b](https://github.com/SolidAlloy/SolidUtilities/commit/ee3da0b2a66c29eb6c5ed2663facd2c02bfdc571))
* Added SerializedPropertyHelper.FindPropertiesOfType() ([fc61190](https://github.com/SolidAlloy/SolidUtilities/commit/fc61190a47cce634b042f6c5a47a1293ed048eb6))

## [1.36.2](https://github.com/SolidAlloy/SolidUtilities/compare/1.36.1...1.36.2) (2022-03-15)


### Bug Fixes

* Fixed ReadOnlySpan not being found in Unity 2020.3 and lower ([b2cc6b2](https://github.com/SolidAlloy/SolidUtilities/commit/b2cc6b215b911a7ed1ce93897ae9b45868bf0b5b))

## [1.36.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.36.0...1.36.1) (2022-03-12)


### Bug Fixes

* Fixed the compilation error when installing SolidUtilities without Stytem.Runtime.CompilerServices ([b447e9c](https://github.com/SolidAlloy/SolidUtilities/commit/b447e9c808c9c352e8b1210cd9e1728b1dd0a3ff))

# [1.36.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.35.0...1.36.0) (2022-02-12)


### Features

* Added EditorHelper.ForceRebuildInspectors() ([8121c2e](https://github.com/SolidAlloy/SolidUtilities/commit/8121c2e22ac23207fdf489d1cfdac88d6c31346d))
* Added TypeHelper.GetNiceNameOfGenericType() method ([2052bcd](https://github.com/SolidAlloy/SolidUtilities/commit/2052bcd997e9ec323edba014b0fbdf317d530d2b))

# [1.35.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.34.0...1.35.0) (2022-02-03)


### Bug Fixes

* Fixed unnecessary garbage allocations in ListHelper.Empty<T> ([8a09a55](https://github.com/SolidAlloy/SolidUtilities/commit/8a09a5585182917c6c57a13536abcbbdb431f2a9))
* Removed the unnecessary requirement for serializable types to have at least one serialized field ([da8e3bf](https://github.com/SolidAlloy/SolidUtilities/commit/da8e3bf00de7fe3da7bed489ec86240bba3d0219))


### Features

* Added an optional icon argument to GUIContentHelper.Temp() ([ede56bd](https://github.com/SolidAlloy/SolidUtilities/commit/ede56bd12488d04e72e8e076a9d3caeb01051f3f))
* Added AssemblyGeneration class useful for generating assemblies that are not supposed to be auto-referenced ([3b30b54](https://github.com/SolidAlloy/SolidUtilities/commit/3b30b54a22ca4378c163cf237454dbc019b29919))
* Added AssetDatabaseHelper.DisabledScope() for disabling asset database temporarily ([4727f9d](https://github.com/SolidAlloy/SolidUtilities/commit/4727f9df5eb05921c325345219000c118481da71))
* Added AssetDatabaseHelper.GetUniqueGUID() method ([b644fd8](https://github.com/SolidAlloy/SolidUtilities/commit/b644fd86b73a20db3bc668e7a979df17c8c30c80))
* Added DrawerWithModes property drawer ([2ebe4e7](https://github.com/SolidAlloy/SolidUtilities/commit/2ebe4e7261fdf226fb575b28801c3b3d2290ec0b))
* Added Enumerable.SelectWhere() extension method ([02c02c9](https://github.com/SolidAlloy/SolidUtilities/commit/02c02c94af6169ce4bc0d1080332650f5c01ea10))
* Added IEnumerable interface for ReadOnlySpan<T> and Split() extension method ([6b6241d](https://github.com/SolidAlloy/SolidUtilities/commit/6b6241d8b4c7dc00b8e4b09139baaa52dd59b9ca))
* Added new icons to EditorIcons: AddButtonS and AddButtonI ([8c19edf](https://github.com/SolidAlloy/SolidUtilities/commit/8c19edfd5dfd2372360c0f017f62f1380e4c2958))
* Added SerializedProperty.GetParent(), GetObjectType, GetFieldInfo, GetObject methods ([ce3394a](https://github.com/SolidAlloy/SolidUtilities/commit/ce3394a1dcb7ec1000020c169affcfd35080a986))
* Added string.GetSubstringBefore(char character) extension method ([79ef45e](https://github.com/SolidAlloy/SolidUtilities/commit/79ef45e39089482ba28abdc695c195afa1f1eff3))
* Added the ArrayHelper class with methods that allow changing the size of the array ([cebdd0f](https://github.com/SolidAlloy/SolidUtilities/commit/cebdd0f601895394c0dd7c015792ea9905fc17a3))
* Added the EditorHelper.GetCurrentMousePosition() method ([8d154df](https://github.com/SolidAlloy/SolidUtilities/commit/8d154df8e6dd4339101f9217ecd65d163c6b9a9c))
* Added the GUIContentHelper.Temp(text, tooltip) method ([585544d](https://github.com/SolidAlloy/SolidUtilities/commit/585544d303e1163f5e11ee6da1d32a34afd794dc))
* Added the string.GetSubstringBeforeLast extension method ([cd45460](https://github.com/SolidAlloy/SolidUtilities/commit/cd45460e41feaabcea62f459386fa0809b28466e))

# [1.34.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.33.1...1.34.0) (2021-11-21)


### Features

* Replaced KeysCollection and ValuesCollection in FastIterationDictionary with IKeysValuesHolder interface ([4bd56af](https://github.com/SolidAlloy/SolidUtilities/commit/4bd56af6014ab97ee4ab184a9f832d1d74e4e150))

## [1.33.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.33.0...1.33.1) (2021-10-22)


### Bug Fixes

* Fixed MissingReferenceException on MacOS ([16a2fd3](https://github.com/SolidAlloy/SolidUtilities/commit/16a2fd3253f16fba03b5dd66b37704f65c1fc249))

# [1.33.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.32.2...1.33.0) (2021-10-13)


### Features

* Added the Rect.Contains(Rect otherRect) extension method ([b860bc5](https://github.com/SolidAlloy/SolidUtilities/commit/b860bc559de57d5947b35e9e46b38445e2ea5448))

## [1.32.2](https://github.com/SolidAlloy/SolidUtilities/compare/1.32.1...1.32.2) (2021-09-29)


### Bug Fixes

* Fixed MissingReferenceException sometimes being reported in EditorIcons on MacOS ([a363c70](https://github.com/SolidAlloy/SolidUtilities/commit/a363c70cb8b700bb6e8ab7b1aad92caea0af905f))

## [1.32.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.32.0...1.32.1) (2021-09-26)


### Bug Fixes

* Fixed the correct type not being found in MonoScript if the class was not the first in the file ([4703bb0](https://github.com/SolidAlloy/SolidUtilities/commit/4703bb06db067e57e94f301d568c571790b56e58))

# [1.32.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.31.0...1.32.0) (2021-08-28)


### Features

* Added ability to draw additional fields in the build settings window ([f6ba5e9](https://github.com/SolidAlloy/SolidUtilities/commit/f6ba5e9085d688f28dc296996af40d768af898d9))

# [1.31.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.30.0...1.31.0) (2021-08-22)


### Bug Fixes

* Moved StackTraceHelper to the Editor assembly ([e2292ca](https://github.com/SolidAlloy/SolidUtilities/commit/e2292ca328b603bb7ff7e6b4533eb16eb44a78d1))


### Features

* Added EditorGUILayoutHelper.DrawErrorMessage() ([ff8c94e](https://github.com/SolidAlloy/SolidUtilities/commit/ff8c94ef2cfafd5b9ce874129c189880931b9096))
* Added ICollection.AddIfMissing() extension method ([6e8b360](https://github.com/SolidAlloy/SolidUtilities/commit/6e8b360eab6cbadc8b8dd2d9488122876a42dd0a))
* Added ListHelper.Empty<T>() method ([b8d29c0](https://github.com/SolidAlloy/SolidUtilities/commit/b8d29c0dc53896dc355953d51f83958a57b55bbb))
* Added PlayModeSaver - utility that allows saving select component fields into edit mode ([ac9b1fd](https://github.com/SolidAlloy/SolidUtilities/commit/ac9b1fda486accfbe741752f7dd6b8110d32173d))
* Added StackTraceHelper.EnvironmentToUnityStyle() and AddLinks() methods ([38d11e9](https://github.com/SolidAlloy/SolidUtilities/commit/38d11e9c5043ff6c6301ab77d3fd349efeb3a9d9))
* Added string.IndexOfNth() extension method ([741fbbe](https://github.com/SolidAlloy/SolidUtilities/commit/741fbbece90362b6ad986c6c608f355f2b3ad445))
* Refactored DrawHelpers and moved their methods to classes with more familiar names ([262d982](https://github.com/SolidAlloy/SolidUtilities/commit/262d982211b1ea3fdab4f81b57bd4e2246450343))

# [1.30.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.29.0...1.30.0) (2021-06-20)


### Features

* Added PathHelper.MakeRelative() and PathHelper.IsSubPathOf() methods ([7f9803f](https://github.com/SolidAlloy/SolidUtilities/commit/7f9803f6259aea6d0c446c51bd7a7b96ce7ba0f8))
* Added ProjectWindowUtilProxy.GetActiveFolderPath() method ([fae929f](https://github.com/SolidAlloy/SolidUtilities/commit/fae929f67e2b53f4a8c2df71139e7132772d0d17))

# [1.29.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.28.2...1.29.0) (2021-04-30)


### Bug Fixes

* Fixed MissingMethodException when using DelayedPropertyField in Unity 2021 ([a5bfb94](https://github.com/SolidAlloy/SolidUtilities/commit/a5bfb9445724f220375651b414c766896ea69c5d))


### Features

* Added a SerializedProperty.GetFieldInfoAndType() extension method ([ff2af73](https://github.com/SolidAlloy/SolidUtilities/commit/ff2af73ea4012740301a8be66613507aee9a7c5b))
* Added DelayedPropertyField(Rect, SerializedProperty, GUIContent, bool) method ([414909d](https://github.com/SolidAlloy/SolidUtilities/commit/414909d475fc57dead092d7426ccdbb7a96be06c))
* Added EditorDrawHelper.LabelWidth disposable struct ([fb07ef9](https://github.com/SolidAlloy/SolidUtilities/commit/fb07ef962b564cb29ae3a9d127e6f85d57929508))
* Added EditorGUIHelper.HasKeyboardFocus(int controlID) method ([9428a87](https://github.com/SolidAlloy/SolidUtilities/commit/9428a87fc6522d275b3948a352e2420fc5bbed15))
* Added IDelayable interface that can be used on custom PropertyDrawers to be drawn as delayable ([9a573ac](https://github.com/SolidAlloy/SolidUtilities/commit/9a573aca037e593ffc38476b13163236f2ccd3bd))
* Added Rect.ShiftLinesDown(int linesNum) method ([9c95b8a](https://github.com/SolidAlloy/SolidUtilities/commit/9c95b8acb17634eeecd8c6403c25b845e482d1b6))
* Replaced Rect.ShiftLindesDown with Rect.ShiftOneLineDown method ([0c048a9](https://github.com/SolidAlloy/SolidUtilities/commit/0c048a90a1bbfe6867944871efda093c4c85b9ed))

## [1.28.2](https://github.com/SolidAlloy/SolidUtilities/compare/1.28.1...1.28.2) (2021-04-15)


### Performance Improvements

* Reduced garbage allocation in the Object.DeepCopy() method ([4184431](https://github.com/SolidAlloy/SolidUtilities/commit/4184431ad226b2c7cc0e4edbba6d2f4253f3bcef))

## [1.28.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.28.0...1.28.1) (2021-04-15)


### Bug Fixes

* Fixed MissingMethodException when calling EditorGUILayoutHelper.DelayedPropertyField() ([05b1a00](https://github.com/SolidAlloy/SolidUtilities/commit/05b1a001d00ccdd371d1dcd577b909d9f28ba4dc))

# [1.28.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.27.0...1.28.0) (2021-03-19)


### Features

* Added Dictionary.TryGetTypedValue method ([1459966](https://github.com/SolidAlloy/SolidUtilities/commit/14599669ec56f51898860f8f62802443e69ee19f))
* Added the iterationCount parameter to Timer ([a7e6c92](https://github.com/SolidAlloy/SolidUtilities/commit/a7e6c92988b0bd410b75057bb2f20fc03f7c8eda))

# [1.27.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.26.1...1.27.0) (2021-03-01)


### Bug Fixes

* Fixed compilation errors in Enum.ContainsFlag for C# 7.0 and below ([4cf1c63](https://github.com/SolidAlloy/SolidUtilities/commit/4cf1c63467a4f4db3e5097a56eb9d046e777077c))


### Features

* Added ReplaceDefaultDrawer method ([67c6f8e](https://github.com/SolidAlloy/SolidUtilities/commit/67c6f8ecfc7de42970b26429b9bcd0569af70f91))

## [1.26.1](https://github.com/SolidAlloy/SolidUtilities/compare/1.26.0...1.26.1) (2021-02-19)


### Bug Fixes

* Removed 2020 suffix from UnityEditorInternals ([ae579b5](https://github.com/SolidAlloy/SolidUtilities/commit/ae579b5f73e12eca2181584c3810ec9b62e3d4c6))
* Removed a duplicate using statement ([1fda48e](https://github.com/SolidAlloy/SolidUtilities/commit/1fda48e4f314432c0071082a71688777c3bc4210))

# [1.26.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.25.0...1.26.0) (2021-02-15)


### Features

* Added SerializedProperty.HasCustomPropertyDrawer() method ([101a14a](https://github.com/SolidAlloy/SolidUtilities/commit/101a14a7f6e26874902b7cece18c5d44f3035deb))

# [1.25.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.24.0...1.25.0) (2021-02-10)


### Features

* Added EditorDrawHelper.DelayedPropertyField method ([c18ce85](https://github.com/SolidAlloy/SolidUtilities/commit/c18ce85643509a3c3dc23841a35950c44a4daa91))
* Added object.ShallowCopy() and object.DeepCopy() methods ([0afac69](https://github.com/SolidAlloy/SolidUtilities/commit/0afac69bb420eb729ec91ea6564e40cfeb805fc3))

# [1.24.0](https://github.com/SolidAlloy/SolidUtilities/compare/1.23.1...1.24.0) (2021-02-01)


### Features

* Added EditorDrawHelper.IndentLevel disposable struct ([2a82ec8](https://github.com/SolidAlloy/SolidUtilities/commit/2a82ec8f6b8fa6619269d2e604ddb252b7a1e07e))

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
