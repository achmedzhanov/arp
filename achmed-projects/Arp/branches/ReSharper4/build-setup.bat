rem build Solution
%windir%\Microsoft.NET\Framework\v3.5\msbuild.exe Arp.Setup/Arp.Setup.proj  /p:Configuration=Debug  /p:CreatePackage=true /verbosity:d