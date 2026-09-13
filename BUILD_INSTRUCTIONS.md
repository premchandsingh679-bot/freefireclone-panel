# 📱 APK Build Instructions - Free Fire Client Panel

## Prerequisites

Before building the APK, ensure you have the following installed:

### 1. **Visual Studio or Visual Studio Code**
   - Download: https://visualstudio.microsoft.com/
   - Install with "Mobile development with .NET" workload
   - OR Visual Studio for Mac (https://visualstudio.microsoft.com/vs/mac/)

### 2. **.NET SDK 6.0 or higher**
   - Download: https://dotnet.microsoft.com/download
   - Verify installation:
     ```bash
     dotnet --version
     ```

### 3. **Android SDK**
   - Installed automatically with Visual Studio
   - Or download from: https://developer.android.com/studio
   - Minimum API Level 21 (Android 5.0)

### 4. **Java Development Kit (JDK)**
   - Required for Android development
   - Download: https://www.oracle.com/java/technologies/downloads/

---

## Method 1: Build with Visual Studio (Easiest)

### Windows / Mac Steps:

1. **Open the project**
   ```
   File → Open → Select FreeFireClientPanel.csproj
   ```

2. **Select Release Configuration**
   - Top toolbar: Change "Debug" to "Release"
   - Platform: Select "Android"

3. **Build the Project**
   ```
   Build → Build Solution
   ```
   Or press: `Ctrl+Shift+B`

4. **Create Release Package**
   ```
   Build → Publish Selection
   ```
   Or right-click project → Publish

5. **Find the APK**
   ```
   bin/Release/net6.0-android/com.freefireclone.panel.apk
   ```

6. **Install on Device**
   ```
   Build → Deploy Solution
   ```
   Or use ADB:
   ```bash
   adb install bin/Release/net6.0-android/com.freefireclone.panel.apk
   ```

---

## Method 2: Build with Command Line

### Step 1: Clone/Download Project
```bash
git clone https://github.com/premchandsingh679-bot/freefireclone-panel.git
cd freefireclone-panel
```

### Step 2: Restore Dependencies
```bash
dotnet restore
```

### Step 3: Build Debug APK (for testing)
```bash
dotnet build -c Debug -f net6.0-android
```

### Step 4: Build Release APK (for distribution)
```bash
dotnet build -c Release -f net6.0-android
```

### Step 5: Publish APK
```bash
dotnet publish -c Release -f net6.0-android
```

### Step 6: Find the APK
The APK will be located at:
```
bin/Release/net6.0-android/com.freefireclone.panel.apk
```

---

## Method 3: Using the Build Script

### On Linux/Mac:
```bash
chmod +x build-release.sh
./build-release.sh
```

### On Windows (PowerShell):
```powershell
.\build-release.bat
```

---

## Installing the APK

### Option 1: Using ADB (Android Debug Bridge)

1. **Enable Developer Mode on Android Device**
   - Settings → About Phone → Tap "Build Number" 7 times
   - Settings → Developer Options → Enable USB Debugging

2. **Connect Device via USB**
   - Plug phone into computer
   - Choose "Transfer files" or "PTP" mode

3. **Install APK**
   ```bash
   adb install -r bin/Release/net6.0-android/com.freefireclone.panel.apk
   ```

4. **Verify Installation**
   ```bash
   adb shell pm list packages | grep freefireclone
   ```

5. **Launch App**
   ```bash
   adb shell am start -n com.freefireclone.panel/FreeFireUIDemo.MainActivity
   ```

### Option 2: Direct File Transfer
1. Copy APK file to Android device via USB
2. Open file manager on phone
3. Tap the APK file
4. Tap "Install"

### Option 3: Visual Studio Deploy
1. Select device in toolbar dropdown
2. Click "Debug" or "Release" button
3. App will auto-install and launch

---

## Signing the APK

For release distribution, sign your APK:

### Step 1: Create a Keystore
```bash
keytool -genkey -v -keystore my-release-key.keystore \
  -keyalg RSA -keysize 2048 -validity 10000 \
  -alias my-key-alias
```

### Step 2: Sign the APK
```bash
jarsigner -verbose -sigalg MD5withRSA -digestalg SHA1 \
  -keystore my-release-key.keystore \
  bin/Release/net6.0-android/com.freefireclone.panel.apk \
  my-key-alias
```

### Step 3: Align the APK
```bash
zipalign -v 4 \
  bin/Release/net6.0-android/com.freefireclone.panel.apk \
  com.freefireclone.panel-signed-aligned.apk
```

---

## Troubleshooting

### ❌ Error: "Android SDK not found"
**Solution:**
```bash
dotnet workload restore
dotnet workload install android
```

### ❌ Error: "Java not found"
**Solution:**
- Install JDK: https://www.oracle.com/java/technologies/downloads/
- Set JAVA_HOME environment variable:
  - Windows: `setx JAVA_HOME "C:\Program Files\Java\jdk-21"`
  - Mac/Linux: `export JAVA_HOME=$(/usr/libexec/java_home)`

### ❌ Error: "API Level 21 not found"
**Solution:**
- Open Android SDK Manager
- Install "Android 5.0 (API 21)" or higher

### ❌ Error: ".NET workload not found"
**Solution:**
```bash
dotnet workload update
dotnet workload install maui-android
```

### ❌ APK won't install on device
**Solution:**
- Uninstall existing version: `adb uninstall com.freefireclone.panel`
- Enable "Unknown Sources" in Android settings
- Use `-r` flag to reinstall: `adb install -r app.apk`

---

## APK Specifications

After successful build:

| Property | Value |
|----------|-------|
| **Package Name** | com.freefireclone.panel |
| **App Name** | Free Fire Client |
| **Min API Level** | 21 (Android 5.0) |
| **Target API Level** | 33 (Android 13) |
| **APK Size** | ~15-25 MB (Debug: ~50+ MB) |
| **Architecture** | arm64-v8a (64-bit) |

---

## Testing the APK

### On Android Emulator:
```bash
# List available emulators
emulator -list-avds

# Launch emulator
emulator -avd Pixel_4_API_31 &

# Install APK
adb install -r bin/Release/net6.0-android/com.freefireclone.panel.apk

# Check logs
adb logcat
```

### On Physical Device:
1. Connect via USB
2. Enable Developer Mode
3. Enable USB Debugging
4. Install using `adb install` command
5. Check permissions in Settings → Apps

---

## Publishing to Google Play Store

1. **Sign APK** (see Signing section above)
2. **Create Google Play Account** ($25 one-time fee)
3. **Upload APK** to Google Play Console
4. **Fill in App Details**:
   - Title
   - Description
   - Screenshots
   - Permissions
5. **Submit for Review**
6. **Wait for approval** (24-48 hours usually)

---

## Quick Reference Commands

```bash
# Restore packages
dotnet restore

# Build Debug
dotnet build -c Debug -f net6.0-android

# Build Release
dotnet build -c Release -f net6.0-android

# Publish Release
dotnet publish -c Release -f net6.0-android

# Install on device
adb install -r bin/Release/net6.0-android/com.freefireclone.panel.apk

# Uninstall
adb uninstall com.freefireclone.panel

# Launch app
adb shell am start -n com.freefireclone.panel/FreeFireUIDemo.MainActivity

# View logs
adb logcat
```

---

## Need Help?

- **Official Docs**: https://learn.microsoft.com/en-us/dotnet/maui/
- **Android Dev**: https://developer.android.com/
- **GitHub Issues**: https://github.com/premchandsingh679-bot/freefireclone-panel/issues

---

**✨ Your APK is ready to go!**
