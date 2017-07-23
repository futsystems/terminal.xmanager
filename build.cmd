%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild /target:BuildClean build.proj
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild /target:Build build.proj
%SYSTEMROOT%\Microsoft.NET\Framework\v4.0.30319\msbuild /target:Publish build.proj

"dotNET_Reactor.exe" -project "xconsole.nrproj"
"dotNET_Reactor.exe" -project "xconsole-addin.nrproj"