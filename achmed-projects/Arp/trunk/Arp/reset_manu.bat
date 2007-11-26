
reg delete HKEY_CURRENT_USER\Software\Microsoft\VisualStudio\8.0 /f /v ReSharper_OneTimeInitializationDoneForBuild

reg delete HKEY_LOCAL_MACHINE\SOFTWARE\JetBrains\ReSharper\v3.0\vs8.0  /f /v "One-Time Initialization Generation"
