using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Timers;

namespace FreeFireUIDemo
{
    [Activity(
        Label = "Free Fire Client",
        MainLauncher = true,
        Theme = "@android:style/Theme.Material.NoActionBar"
    )]
    public class MainActivity : Activity
    {
        private Switch esSwitch;
        private Switch aimSwitch;
        private Switch flSwitch;
        private TextView esStatus;
        private TextView aimStatus;
        private TextView flStatus;
        private TextView statusText;
        private TextView statusTime;
        private TextView activeCount;
        private TextView uptimeCount;
        private Button startBtn;
        private Button stopBtn;

        private bool isClientRunning = false;
        private int activeFeaturesCount = 0;
        private Timer uptimeTimer;
        private int uptimeSeconds = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // Initialize UI Elements
            InitializeViews();

            // Load saved preferences
            LoadPreferences();

            // Setup Switch Change Listeners
            SetupSwitchListeners();

            // Setup Button Listeners
            SetupButtonListeners();
        }

        private void InitializeViews()
        {
            esSwitch = FindViewById<Switch>(Resource.Id.esSwitch);
            aimSwitch = FindViewById<Switch>(Resource.Id.aimSwitch);
            flSwitch = FindViewById<Switch>(Resource.Id.flSwitch);
            esStatus = FindViewById<TextView>(Resource.Id.esStatus);
            aimStatus = FindViewById<TextView>(Resource.Id.aimStatus);
            flStatus = FindViewById<TextView>(Resource.Id.flStatus);
            statusText = FindViewById<TextView>(Resource.Id.statusText);
            statusTime = FindViewById<TextView>(Resource.Id.statusTime);
            activeCount = FindViewById<TextView>(Resource.Id.activeCount);
            uptimeCount = FindViewById<TextView>(Resource.Id.uptimeCount);
            startBtn = FindViewById<Button>(Resource.Id.startBtn);
            stopBtn = FindViewById<Button>(Resource.Id.stopBtn);
        }

        private void SetupSwitchListeners()
        {
            esSwitch.CheckedChange += (s, e) =>
            {
                HandleESToggle(e.IsChecked);
            };

            aimSwitch.CheckedChange += (s, e) =>
            {
                HandleAIMToggle(e.IsChecked);
            };

            flSwitch.CheckedChange += (s, e) =>
            {
                HandleFLToggle(e.IsChecked);
            };
        }

        private void SetupButtonListeners()
        {
            startBtn.Click += (s, e) =>
            {
                StartClient();
            };

            stopBtn.Click += (s, e) =>
            {
                StopClient();
            };
        }

        private void HandleESToggle(bool isChecked)
        {
            if (isChecked)
            {
                esStatus.Text = "Status: ACTIVE ✓";
                esStatus.SetTextColor(Android.Graphics.Color.ParseColor("#65D88A"));
                activeFeaturesCount++;
                ShowToast("Enemy Sensor ACTIVATED");
            }
            else
            {
                esStatus.Text = "Status: INACTIVE";
                esStatus.SetTextColor(Android.Graphics.Color.ParseColor("#FF6B6B"));
                activeFeaturesCount--;
                ShowToast("Enemy Sensor DEACTIVATED");
            }

            UpdateActiveCount();
            SavePreferences();
        }

        private void HandleAIMToggle(bool isChecked)
        {
            if (isChecked)
            {
                aimStatus.Text = "Status: ACTIVE ✓";
                aimStatus.SetTextColor(Android.Graphics.Color.ParseColor("#65D88A"));
                activeFeaturesCount++;
                ShowToast("Auto Aim ACTIVATED");
            }
            else
            {
                aimStatus.Text = "Status: INACTIVE";
                aimStatus.SetTextColor(Android.Graphics.Color.ParseColor("#FF6B6B"));
                activeFeaturesCount--;
                ShowToast("Auto Aim DEACTIVATED");
            }

            UpdateActiveCount();
            SavePreferences();
        }

        private void HandleFLToggle(bool isChecked)
        {
            if (isChecked)
            {
                flStatus.Text = "Status: ACTIVE ✓";
                flStatus.SetTextColor(Android.Graphics.Color.ParseColor("#65D88A"));
                activeFeaturesCount++;
                ShowToast("Flash Light ACTIVATED");
            }
            else
            {
                flStatus.Text = "Status: INACTIVE";
                flStatus.SetTextColor(Android.Graphics.Color.ParseColor("#FF6B6B"));
                activeFeaturesCount--;
                ShowToast("Flash Light DEACTIVATED");
            }

            UpdateActiveCount();
            SavePreferences();
        }

        private void StartClient()
        {
            if (isClientRunning)
            {
                ShowToast("Client already running!");
                return;
            }

            isClientRunning = true;
            uptimeSeconds = 0;

            statusText.Text = "● CONNECTED";
            statusText.SetTextColor(Android.Graphics.Color.ParseColor("#65D88A"));
            statusTime.Text = "Connected at: " + DateTime.Now.ToString("HH:mm:ss");

            startBtn.Enabled = false;
            stopBtn.Enabled = true;

            // Start uptime timer
            uptimeTimer = new Timer(1000);
            uptimeTimer.Elapsed += (s, e) =>
            {
                uptimeSeconds++;
                int minutes = uptimeSeconds / 60;
                int seconds = uptimeSeconds % 60;

                RunOnUiThread(() =>
                {
                    uptimeCount.Text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
                });
            };
            uptimeTimer.Start();

            ShowToast("Client STARTED");
        }

        private void StopClient()
        {
            if (!isClientRunning)
            {
                ShowToast("Client not running!");
                return;
            }

            isClientRunning = false;

            if (uptimeTimer != null)
            {
                uptimeTimer.Stop();
                uptimeTimer.Dispose();
            }

            statusText.Text = "● DISCONNECTED";
            statusText.SetTextColor(Android.Graphics.Color.ParseColor("#FF6B6B"));
            statusTime.Text = "Disconnected at: " + DateTime.Now.ToString("HH:mm:ss");

            startBtn.Enabled = true;
            stopBtn.Enabled = false;

            // Turn off all features
            esSwitch.Checked = false;
            aimSwitch.Checked = false;
            flSwitch.Checked = false;

            ShowToast("Client STOPPED");
        }

        private void UpdateActiveCount()
        {
            activeCount.Text = activeFeaturesCount.ToString();
        }

        private void SavePreferences()
        {
            var preferences = GetSharedPreferences("FFClientPrefs", FileCreationMode.Private);
            var editor = preferences.Edit();

            editor.PutBoolean("ES_STATE", esSwitch.Checked);
            editor.PutBoolean("AIM_STATE", aimSwitch.Checked);
            editor.PutBoolean("FL_STATE", flSwitch.Checked);

            editor.Apply();
        }

        private void LoadPreferences()
        {
            var preferences = GetSharedPreferences("FFClientPrefs", FileCreationMode.Private);

            esSwitch.Checked = preferences.GetBoolean("ES_STATE", false);
            aimSwitch.Checked = preferences.GetBoolean("AIM_STATE", false);
            flSwitch.Checked = preferences.GetBoolean("FL_STATE", false);

            // Update UI based on loaded state
            activeFeaturesCount = 0;
            if (esSwitch.Checked) activeFeaturesCount++;
            if (aimSwitch.Checked) activeFeaturesCount++;
            if (flSwitch.Checked) activeFeaturesCount++;

            UpdateActiveCount();
        }

        private void ShowToast(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        protected override void OnDestroy()
        {
            if (uptimeTimer != null)
            {
                uptimeTimer.Stop();
                uptimeTimer.Dispose();
            }
            base.OnDestroy();
        }
    }
}