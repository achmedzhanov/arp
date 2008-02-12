

%windir%\Microsoft.NET\Framework\v2.0.50727\msbuild.exe Arp-L4N.csproj /p:Configuration=Release

xcopy /Y bin\Release\arp.dll  Z:\public_html\arp\releases\alpha1\
