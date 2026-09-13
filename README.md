# 🔥 Free Fire Client Panel - Android Edition

A fully functional **Free Fire Client Control Panel** built with **Xamarin.Android** and **Material Design**.

## ✨ Features

### 🎮 Core Features
- **ES (Enemy Sensor)** - Real-time enemy detection toggle
- **AIM (Auto Aim)** - Precision targeting system toggle
- **FL (Flash Light)** - Night vision / light enhancement toggle

### 📊 Dashboard Stats
- **Active Features Counter** - Shows how many features are currently enabled
- **Uptime Timer** - Tracks client connection duration
- **Client Status** - Real-time connection indicator
- **Last Update Time** - Shows when status was last updated

### 🎯 User Actions
- **START Button** - Activates the client and begins uptime tracking
- **STOP Button** - Deactivates all features and stops the client
- **Toggle Switches** - Enable/disable individual features on the fly

### 💾 Data Persistence
- All switch states are saved to **SharedPreferences**
- Preferences load automatically on app startup
- Feature states persist between app sessions

### 🔔 User Feedback
- Toast notifications for all actions
- Real-time UI updates
- Color-coded status indicators (Green=Active, Red=Inactive)

## 📱 UI Components

### Colors
- **Background**: `#0F0F12` (Deep Black)
- **Cards**: `#1E1E23` (Dark Gray)
- **Text Primary**: `#FFFFFF` (White)
- **Text Secondary**: `#8E8E96` (Light Gray)
- **Active Status**: `#65D88A` (Green)
- **Inactive Status**: `#FF6B6B` (Red)
- **Accent**: `#FFD700` (Gold)

### Dimensions
- **Padding**: 12-20 dp (consistent spacing)
- **Title**: 28 sp
- **Headers**: 20 sp
- **Body**: 14-16 sp
- **Labels**: 10-12 sp

## 🚀 Getting Started

### Requirements
- Xamarin.Android
- Android API Level 21+ (Android 5.0)
- Visual Studio or Visual Studio for Mac

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/premchandsingh679-bot/freefireclone-panel.git
   cd freefireclone-panel
   ```

2. **Open in Visual Studio**
   - Open the `.sln` file in Visual Studio
   - Restore NuGet packages

3. **Configure AndroidManifest.xml**
   - Update package name if needed
   - Ensure minimum SDK is 21

4. **Build & Deploy**
   ```bash
   dotnet build
   dotnet publish -c Release
   ```

5. **Run on Emulator or Device**
   - Deploy to Android emulator or physical device
   - Launch "Free Fire Client" app

## 📋 File Structure

```
freefireclone-panel/
├── MainActivity.cs                 # Main activity with all logic
├── AndroidManifest.xml            # Android manifest configuration
├── Resources/
│   ├── layout/
│   │   └── activity_main.xml      # Main UI layout
│   └── values/
│       └── strings.xml            # String resources
└── README.md                       # This file
```

## 🎮 Usage

### Starting the Client
1. Click the **START** button
2. Client status changes to "● CONNECTED"
3. Uptime timer begins counting
4. All switches are now enabled

### Enabling Features
- Toggle **ES**, **AIM**, or **FL** switches
- Each toggle shows:
  - Status update (ACTIVE / INACTIVE)
  - Toast notification
  - Active features counter increments
  - Color changes (Green = Active, Red = Inactive)

### Stopping the Client
1. Click the **STOP** button
2. All features automatically disable
3. Client status changes to "● DISCONNECTED"
4. Uptime timer stops

### Saving Preferences
- All switch states are automatically saved
- Changes persist even after closing the app
- Preferences load on app restart

## 🔧 API Methods

### MainActivity Methods

#### `StartClient()`
Activates the client and begins uptime tracking.
```csharp
- Sets isClientRunning = true
- Updates UI status to CONNECTED (Green)
- Starts uptime timer
- Enables all feature switches
```

#### `StopClient()`
Deactivates the client and all features.
```csharp
- Sets isClientRunning = false
- Stops uptime timer
- Updates UI status to DISCONNECTED (Red)
- Disables all feature switches
- Clears uptime counter
```

#### `HandleESToggle(bool isChecked)`
Toggles Enemy Sensor feature.
```csharp
- Updates UI status
- Increments/decrements active features counter
- Shows toast notification
- Saves preferences
```

#### `HandleAIMToggle(bool isChecked)`
Toggles Auto Aim feature.
```csharp
- Updates UI status
- Increments/decrements active features counter
- Shows toast notification
- Saves preferences
```

#### `HandleFLToggle(bool isChecked)`
Toggles Flash Light feature.
```csharp
- Updates UI status
- Increments/decrements active features counter
- Shows toast notification
- Saves preferences
```

#### `SavePreferences()`
Saves all switch states to SharedPreferences.
```csharp
- Uses GetSharedPreferences("FFClientPrefs", FileCreationMode.Private)
- Saves ES_STATE, AIM_STATE, FL_STATE as booleans
```

#### `LoadPreferences()`
Loads saved switch states from SharedPreferences.
```csharp
- Retrieves saved feature states
- Updates UI accordingly
- Recalculates active features count
```

## 📊 Data Flow

```
User Toggle Switch
        ↓
CheckedChange Event Handler
        ↓
Handle[Feature]Toggle() Method
        ↓
Update UI Status & Color
        ↓
Update Active Count
        ↓
SavePreferences()
        ↓
Show Toast Notification
```

## 🛡️ Error Handling

- **Client Already Running**: Shows toast "Client already running!"
- **Client Not Running**: Shows toast "Client not running!"
- **Null Reference Protection**: All timers are checked before disposal

## ⚠️ Important Notes

1. **Educational Purpose Only** - This is a demo/educational project
2. **No Backend Integration** - This is a UI-only mockup
3. **No Real Game Interaction** - Does not interact with actual Free Fire game
4. **Preferences Scope** - SharedPreferences are app-scoped and private

## 🔐 Permissions

```xml
<uses-permission android:name="android.permission.INTERNET" />
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
```

## 🎨 Customization

### Change Colors
Edit `activity_main.xml`:
```xml
android:background="#0F0F12"  <!-- Main background -->
android:textColor="#FFFFFF"   <!-- Text color -->
android:textColor="#65D88A"   <!-- Active status color -->
android:textColor="#FF6B6B"   <!-- Inactive status color -->
```

### Change Feature Names
Edit `Resources/values/strings.xml` and `activity_main.xml`

### Change Dimensions
Edit sizes in `activity_main.xml`:
- `android:textSize="28sp"` - Title size
- `android:layout_height="100dp"` - Card height
- `android:padding="20dp"` - Global padding

## 📝 License

This project is for educational purposes only.

## 👨‍💻 Author

**Premchand Singh**  
[GitHub Profile](https://github.com/premchandsingh679-bot)

---

**⭐ If you find this useful, please star the repository!**