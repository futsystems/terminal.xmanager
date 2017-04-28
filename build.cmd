%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild /target:BuildClean build.proj
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild /target:Build build.proj
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild /target:Publish build.proj

"C:\Program Files\Eziriz\.NET Reactor\dotNET_Reactor.exe" -project "xconsole.nrproj"
"C:\Program Files\Eziriz\.NET Reactor\dotNET_Reactor.exe" -project "xconsole-addin.nrproj"