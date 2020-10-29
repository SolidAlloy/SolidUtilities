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
