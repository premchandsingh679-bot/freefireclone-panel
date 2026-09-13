@echo off
REM Free Fire Client Panel - Complete APK Builder for Windows
REM This script builds a complete, ready-to-install APK

setlocal enabledelayedexpansion

echo.
echo ==========================================
echo 🔥 FREE FIRE CLIENT PANEL - APK BUILDER 🔥
echo ==========================================
echo.

REM Check if dotnet is installed
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ ERROR: dotnet is not installed
    echo Download from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo ✅ .NET SDK found
echo.

REM Step 1: Clean
echo [1/6] Cleaning previous builds...
if exist bin rmdir /s /q bin >nul 2>&1
if exist obj rmdir /s /q obj >nul 2>&1
echo ✅ Clean complete
echo.

REM Step 2: Restore
echo [2/6] Restoring NuGet packages...
call dotnet restore
if %errorlevel% neq 0 (
    echo ❌ Failed to restore packages
    pause
    exit /b 1
)
echo ✅ Packages restored
echo.

REM Step 3: Build Debug
echo [3/6] Building Debug APK...
call dotnet build -c Debug -f net6.0-android
if %errorlevel% neq 0 (
    echo ❌ Debug build failed
    pause
    exit /b 1
)
echo ✅ Debug build complete
echo.

REM Step 4: Build Release
echo [4/6] Building Release APK...
call dotnet build -c Release -f net6.0-android
if %errorlevel% neq 0 (
    echo ❌ Release build failed
    pause
    exit /b 1
)
echo ✅ Release build complete
echo.

REM Step 5: Publish
echo [5/6] Publishing Release APK...
call dotnet publish -c Release -f net6.0-android
if %errorlevel% neq 0 (
    echo ❌ Publish failed
    pause
    exit /b 1
)
echo ✅ Publish complete
echo.

REM Step 6: Verify
echo [6/6] Verifying APK files...
echo.

set APK_DEBUG=bin\Debug\net6.0-android\com.freefireclone.panel.apk
set APK_RELEASE=bin\Release\net6.0-android\com.freefireclone.panel.apk

if exist "%APK_DEBUG%" (
    echo ✅ Debug APK: %APK_DEBUG%
    for %%A in ("%APK_DEBUG%") do (
        echo    Size: %%~zA bytes
    )
) else (
    echo ⚠️  Debug APK not found
)

if exist "%APK_RELEASE%" (
    echo ✅ Release APK: %APK_RELEASE%
    for %%A in ("%APK_RELEASE%") do (
        echo    Size: %%~zA bytes
    )
) else (
    echo ⚠️  Release APK not found
)

echo.
echo ==========================================
echo 🎉 BUILD COMPLETE!
echo ==========================================
echo.

echo 🚀 Installation Commands:
echo.
echo Debug APK:
echo   adb install -r %APK_DEBUG%
echo.
echo Release APK:
echo   adb install -r %APK_RELEASE%
echo.
echo 📱 To install on connected device:
echo   1. Enable USB Debugging in Android settings
echo   2. Connect phone via USB
echo   3. Run adb install command above
echo.
echo ==========================================
echo.

pause
