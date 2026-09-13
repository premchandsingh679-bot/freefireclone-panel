#!/bin/bash

# Free Fire Client Panel - APK Build Script
# This script builds a release APK for Android

echo "=================================================="
echo "🔥 FREE FIRE CLIENT PANEL - APK BUILD SCRIPT 🔥"
echo "=================================================="
echo ""

# Check if .NET/Xamarin is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ ERROR: dotnet is not installed"
    echo "Please install .NET SDK from: https://dotnet.microsoft.com/download"
    exit 1
fi

echo "✅ .NET SDK found"
echo ""

# Clean previous builds
echo "🧹 Cleaning previous builds..."
rm -rf bin/
rm -rf obj/
echo "✅ Clean complete"
echo ""

# Restore NuGet packages
echo "📦 Restoring NuGet packages..."
dotnet restore
echo "✅ Packages restored"
echo ""

# Build for Release
echo "🔨 Building Release APK..."
dotnet build -c Release -f net6.0-android
echo "✅ Build complete"
echo ""

# Publish for Release
echo "📤 Publishing Release APK..."
dotnet publish -c Release -f net6.0-android
echo "✅ Publish complete"
echo ""

# Check if APK was created
if [ -f "bin/Release/net6.0-android/com.freefireclone.panel-Signed.apk" ]; then
    echo "✅ APK created successfully!"
    echo "📍 Location: bin/Release/net6.0-android/com.freefireclone.panel-Signed.apk"
    echo ""
    echo "🚀 Ready to install on device:"
    echo "   adb install bin/Release/net6.0-android/com.freefireclone.panel-Signed.apk"
elif [ -f "bin/Release/net6.0-android/com.freefireclone.panel.apk" ]; then
    echo "✅ APK created successfully!"
    echo "📍 Location: bin/Release/net6.0-android/com.freefireclone.panel.apk"
    echo ""
    echo "🚀 Ready to install on device:"
    echo "   adb install bin/Release/net6.0-android/com.freefireclone.panel.apk"
else
    echo "❌ APK was not created. Check build output for errors."
    exit 1
fi

echo ""
echo "=================================================="
echo "✨ BUILD COMPLETE!"
echo "=================================================="
