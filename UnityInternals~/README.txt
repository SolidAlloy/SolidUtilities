Projects in this solution contain calls to Unity internal methods. They are highlighted as errors but upon building the project, those errors will be ignored.
After building a project, the DLL file will be copied over to the parent folder of UnityInternals~, so you will be able to use the updated DLL in Unity after building it.
Projects a built with help of OpenSesame. Check it out at this link https://github.com/mob-sakai/OpenSesame

During development, you can choose which version of the DLL to build: for 2019, or for 2020 or newer. It depends on the Unity version you are developing the plugin in.
However, when publishing the plugin, please build the solution for both Unity2019 and Unity2020 so that it works in all versions.