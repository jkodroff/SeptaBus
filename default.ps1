task default -depends RestoreSolutionPackages, Clean, Test

# This is necessary in order to pull down the nUnit.runners executable, 
# which is a solution-level package and won't be restored like project-level
# packages are upon compile from the command line.
task RestoreSolutionPackages {
    exec { .\.nuget\NuGet.exe restore .\.nuget\packages.config -PackagesDirectory packages }
}

task Clean {
    exec { cmd.exe /c "CleanSolution.bat" }
}

task Compile {
    exec { msbuild SeptaBus.sln }
}

task Test -depends Compile {
  exec { & "packages\NUnit.Runners.2.6.3\tools\nunit-console.exe SeptaBus.Core.Tests\bin\Debug\SeptaBus.Core.Tests.dll SeptaBus.StructureMap.Tests\bin\Debug\SeptaBus.StructureMap.Tests.dll  /nologo"   }
}
 