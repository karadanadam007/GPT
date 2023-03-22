// Decompiled with JetBrains decompiler
// Type: Sirenix.Serialization.Utilities.Editor.AssemblyImportSettingsUtilitiesCustom
// Assembly: Sirenix.Serialization, Version=2.1.13.0, Culture=neutral, PublicKeyToken=null
// MVID: 2CDC14C9-6109-4989-9F36-C750639506EA
// Assembly location: /Users/maglabvolkan/UnityProjects/MagiclabTemplate/Assets/ThirdParty/Sirenix/Assemblies/Sirenix.Serialization.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Sirenix.Serialization.Utilities.Editor
{
  public static class AssemblyImportSettingsUtilitiesCustom
  {
    private static MethodInfo getPropertyIntMethod = typeof (PlayerSettings).GetMethod("GetPropertyInt", BindingFlags.Public | BindingFlags.Static, (Binder) null, new System.Type[2]
    {
      typeof (string),
      typeof (BuildTargetGroup)
    }, (ParameterModifier[]) null);
    private static MethodInfo getScriptingBackendMethod = typeof (PlayerSettings).GetMethods(BindingFlags.Public | BindingFlags.Static).Where(x => x.Name == "GetScriptingBackend" && x.GetParameters().Length > 0).FirstOrDefault(x => x.IsGenericMethod);
    private static MethodInfo getApiCompatibilityLevelMethod = typeof (PlayerSettings).GetMethod("GetApiCompatibilityLevel", BindingFlags.Public | BindingFlags.Static, (Binder) null, new System.Type[1]
    {
      typeof (BuildTargetGroup)
    }, (ParameterModifier[]) null);
    private static MethodInfo apiCompatibilityLevelProperty = typeof (PlayerSettings).GetProperty("apiCompatibilityLevel", BindingFlags.Public | BindingFlags.Static)?.GetGetMethod();
    public static readonly ImmutableList<BuildTarget> Platforms = new ImmutableList<BuildTarget>((IList<BuildTarget>) Enum.GetValues(typeof (BuildTarget)).Cast<BuildTarget>().Where<BuildTarget>((Func<BuildTarget, bool>) (t => t >= ~BuildTarget.iOS && !typeof (BuildTarget).GetMember(t.ToString())[0].IsDefined(typeof (ObsoleteAttribute), false))).ToArray<BuildTarget>());
    public static readonly ImmutableList<BuildTarget> JITPlatforms = new ImmutableList<BuildTarget>((IList<BuildTarget>) AssemblyImportSettingsUtilitiesCustom.Platforms.Where<BuildTarget>((Func<BuildTarget, bool>) (i => i.ToString().StartsWith("StandaloneOSX")))
      .Append(BuildTarget.StandaloneWindows).Append(BuildTarget.StandaloneWindows64).Append(BuildTarget.StandaloneLinux64).Append(BuildTarget.Android).ToArray<BuildTarget>());
    public static readonly ImmutableList<ScriptingImplementation> JITScriptingBackends = new ImmutableList<ScriptingImplementation>((IList<ScriptingImplementation>) new ScriptingImplementation[1]);
    public static readonly ImmutableList<ApiCompatibilityLevel> JITApiCompatibilityLevels;

    static AssemblyImportSettingsUtilitiesCustom()
    {
      string[] source = new string[5]
      {
        "NET_2_0",
        "NET_2_0_Subset",
        "NET_4_6",
        "NET_Web",
        "NET_Micro"
      };
      string[] apiLevelNames = Enum.GetNames(typeof (ApiCompatibilityLevel));
      AssemblyImportSettingsUtilitiesCustom.JITApiCompatibilityLevels = new ImmutableList<ApiCompatibilityLevel>((IList<ApiCompatibilityLevel>) ((IEnumerable<string>) source).Where<string>((Func<string, bool>) (x => ((IEnumerable<string>) apiLevelNames).Contains<string>(x))).Select<string, ApiCompatibilityLevel>((Func<string, ApiCompatibilityLevel>) (x => (ApiCompatibilityLevel) Enum.Parse(typeof (ApiCompatibilityLevel), x))).ToArray<ApiCompatibilityLevel>());
    }

    public static void SetAssemblyImportSettings(
      BuildTarget platform,
      string assemblyFilePath,
      OdinAssemblyImportSettings importSettings)
    {
      bool includeInBuild = false;
      bool includeInEditor = false;
      switch (importSettings)
      {
        case OdinAssemblyImportSettings.IncludeInBuildOnly:
          includeInBuild = true;
          break;
        case OdinAssemblyImportSettings.IncludeInEditorOnly:
          includeInEditor = true;
          break;
        case OdinAssemblyImportSettings.IncludeInAll:
          includeInBuild = true;
          includeInEditor = true;
          break;
      }
      AssemblyImportSettingsUtilitiesCustom.SetAssemblyImportSettings(platform, assemblyFilePath, includeInBuild, includeInEditor);
    }

    public static void SetAssemblyImportSettings(
      BuildTarget platform,
      string assemblyFilePath,
      bool includeInBuild,
      bool includeInEditor)
    {
      PluginImporter pluginImporter = File.Exists(assemblyFilePath) ? (PluginImporter) AssetImporter.GetAtPath(assemblyFilePath) : throw new FileNotFoundException(assemblyFilePath);
      if ((UnityEngine.Object) pluginImporter == (UnityEngine.Object) null)
        throw new InvalidOperationException("Failed to get PluginImporter for " + assemblyFilePath);
      if (!pluginImporter.GetCompatibleWithAnyPlatform() && pluginImporter.GetCompatibleWithPlatform(platform) == includeInBuild && pluginImporter.GetCompatibleWithEditor() == includeInEditor)
        return;
      pluginImporter.SetCompatibleWithAnyPlatform(false);
      pluginImporter.SetCompatibleWithPlatform(platform, includeInBuild);
      pluginImporter.SetCompatibleWithEditor(includeInEditor);
      pluginImporter.SaveAndReimport();
    }

    public static ScriptingImplementation GetCurrentScriptingBackend()
    {
      BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
      if ((object) AssemblyImportSettingsUtilitiesCustom.getScriptingBackendMethod != null)
        return (ScriptingImplementation) AssemblyImportSettingsUtilitiesCustom.getScriptingBackendMethod.Invoke((object) null, new object[1]
        {
          (object) buildTargetGroup
        });
      return (object) AssemblyImportSettingsUtilitiesCustom.getPropertyIntMethod != null ? (ScriptingImplementation) AssemblyImportSettingsUtilitiesCustom.getPropertyIntMethod.Invoke((object) null, new object[2]
      {
        (object) "ScriptingBackend",
        (object) buildTargetGroup
      }) : throw new InvalidOperationException("Was unable to get the current scripting backend!");
    }

    public static ApiCompatibilityLevel GetCurrentApiCompatibilityLevel()
    {
      if ((object) AssemblyImportSettingsUtilitiesCustom.getApiCompatibilityLevelMethod != null)
      {
        BuildTargetGroup buildTargetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
        return (ApiCompatibilityLevel) AssemblyImportSettingsUtilitiesCustom.getApiCompatibilityLevelMethod.Invoke((object) null, new object[1]
        {
          (object) buildTargetGroup
        });
      }
      return (object) AssemblyImportSettingsUtilitiesCustom.apiCompatibilityLevelProperty != null ? (ApiCompatibilityLevel) AssemblyImportSettingsUtilitiesCustom.apiCompatibilityLevelProperty.Invoke((object) null, (object[]) null) : throw new InvalidOperationException("Was unable to get the current api compatibility level!");
    }

    public static bool PlatformSupportsJIT(BuildTarget platform) => AssemblyImportSettingsUtilitiesCustom.JITPlatforms.Contains(platform);

    public static bool ScriptingBackendSupportsJIT(ScriptingImplementation backend) => AssemblyImportSettingsUtilitiesCustom.JITScriptingBackends.Contains(backend);

    public static bool ApiCompatibilityLevelSupportsJIT(ApiCompatibilityLevel apiLevel) => AssemblyImportSettingsUtilitiesCustom.JITApiCompatibilityLevels.Contains(apiLevel);

    public static bool IsJITSupported(
      BuildTarget platform,
      ScriptingImplementation backend,
      ApiCompatibilityLevel apiLevel)
    {
      return AssemblyImportSettingsUtilitiesCustom.PlatformSupportsJIT(platform) && AssemblyImportSettingsUtilitiesCustom.ScriptingBackendSupportsJIT(backend) && AssemblyImportSettingsUtilitiesCustom.ApiCompatibilityLevelSupportsJIT(apiLevel);
    }
  }
}
