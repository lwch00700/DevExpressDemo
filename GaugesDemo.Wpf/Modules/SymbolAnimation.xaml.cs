using System;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Gauges;
using DevExpress.Utils;
using DevExpress.Mvvm;

namespace GaugesDemo {
    public partial class SymbolAnimation : GaugesDemoModule {
        PlayerDataGenerator dataGenerator = new PlayerDataGenerator();

        public SymbolAnimation() {
            InitializeComponent();
            receiverGrid.DataContext = dataGenerator;
        }

        void SrcButton_Click(object sender, RoutedEventArgs e) {
            dataGenerator.ChangeSource();
        }
        void lbeAnimationDirection_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            bool isRightToLeft = (lbeAnimationDirection.SelectedIndex == 0);
            dataGenerator.ChangeText(isRightToLeft);
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e) {
            dataGenerator.SwitchFirstTrack();
        }
        private void LastButton_Click(object sender, RoutedEventArgs e) {
            dataGenerator.SwitchLastTrack();
        }
        private void NextButton_Click(object sender, RoutedEventArgs e) {
            dataGenerator.SwitchNextTrack();
        }
        private void PreviousButton_Click(object sender, RoutedEventArgs e) {
            dataGenerator.SwitchPreviousTrack();
        }
    }
    public class PlayerDataGenerator : BindableBase {
        public string PlayerText {
            get { return GetProperty(() => PlayerText); }
            set { SetProperty(() => PlayerText, value); }
        }
        public string TimeText {
            get { return GetProperty(() => TimeText); }
            set { SetProperty(() => TimeText, value); }
        }
        public SymbolsAnimation PlayerAnimation {
            get { return GetProperty(() => PlayerAnimation); }
            set { SetProperty(() => PlayerAnimation, value); }
        }

        const int animationRefreshTime = 200;
        const string rightToLeftRadioText = "RADIO            NOW PLAYING        WAAF FM BOSTON        107.3 MHZ";
        const string leftToRightRadioText = "107.3 MHZ         WAAF FM BOSTON        NOW PLAYING        RADIO";
        const string rightToLeftCDSourceInfo = "CD          NOW PLAYING              ";
        const string leftToRightCDSourceInfo = "               NOW PLAYING              CD";
        const string rightToLeftTrackInfo = "          AT 320 KBPS         MP3/WMA";
        const string leftToRightTrackInfo = "MP3/WMA          AT 320 KBPS         ";

        static CreepingLineAnimation creepingAnimationLeftToRight = new CreepingLineAnimation() { Direction = CreepingLineDirection.LeftToRight, RefreshTime = TimeSpan.FromMilliseconds(animationRefreshTime), Enable = true, Repeat = true };
        static CreepingLineAnimation creepingAnimationRightToLeft = new CreepingLineAnimation() { Direction = CreepingLineDirection.RightToLeft, RefreshTime = TimeSpan.FromMilliseconds(animationRefreshTime), Enable = true, Repeat = true };
        static BlinkingAnimation blinkingAnimation = new BlinkingAnimation() { RefreshTime = TimeSpan.FromMilliseconds(300), Enable = true };
        static string[] rightToLeftTracks = { "THE DARK SIDE OF THE MOON       PINK FLOYD", "SMOKE ON THE WATER       DEEP PURPLE", "BLACK MOUNTAIN SIDE       LED ZEPPELIN", "TRANSILVANIA       IRON MAIDEN", "HARD ROAD       BLACK SABBATH" };
        static string[] leftToRightTracks = { "PINK FLOYD       THE DARK SIDE OF THE MOON", "DEEP PURPLE       SMOKE ON THE WATER", "LED ZEPPELIN       BLACK MOUNTAIN SIDE", "IRON MAIDEN       TRANSILVANIA", "BLACK SABBATH       HARD ROAD" };


        bool isRadioPlaying = true;
        bool isRightToLeft = true;
        int currentTrack;
        string rightToLeftCDText = "";
        string leftToRightCDText = "";
        DispatcherTimer timeTimer = new DispatcherTimer();
        DispatcherTimer blinkingTimer = new DispatcherTimer();

        public PlayerDataGenerator() {
            PlayerText = "";
            TimeText = "";
            PlayerAnimation = creepingAnimationRightToLeft;
            PlayerText = rightToLeftRadioText;
            timeTimer.Tick += new EventHandler(OnTimedEvent);
            blinkingTimer.Tick += new EventHandler(OnBlinkingTimedEvent);
            timeTimer.Interval = new TimeSpan(0, 0, 1);
            blinkingTimer.Interval = new TimeSpan(0, 0, 4);
            UpdateTime();
            timeTimer.Start();
        }
        void OnTimedEvent(object source, EventArgs e) {
            UpdateTime();
        }
        void OnBlinkingTimedEvent(object source, EventArgs e) {
            blinkingTimer.Stop();
            ChangeText(isRightToLeft);
        }
        void UpdateTime() {
            TimeText = string.Format("{0:H:mm}", DateTime.Now);
        }
        public void ChangeSource() {
            isRadioPlaying = !isRadioPlaying;
            if(isRadioPlaying) {
                PlayerAnimation = isRightToLeft ? creepingAnimationRightToLeft : creepingAnimationLeftToRight;
                PlayerText = isRightToLeft ? rightToLeftRadioText : leftToRightRadioText;
            } else {
                PlayerAnimation = blinkingAnimation;
                PlayerText = "READING";
                blinkingTimer.Start();
            }
        }
        public void ChangeText(bool isAnimationDirectionRightToLeft) {
            isRightToLeft = isAnimationDirectionRightToLeft;
            if(!blinkingTimer.IsEnabled) {
                PlayerAnimation = isRightToLeft ? creepingAnimationRightToLeft : creepingAnimationLeftToRight;
                if(isRadioPlaying)
                    PlayerText = isRightToLeft ? rightToLeftRadioText : leftToRightRadioText;
                else {
                    rightToLeftCDText = rightToLeftCDSourceInfo + rightToLeftTracks[currentTrack] + rightToLeftTrackInfo;
                    leftToRightCDText = leftToRightTrackInfo + leftToRightTracks[currentTrack] + leftToRightCDSourceInfo;
                    PlayerText = isRightToLeft ? rightToLeftCDText : leftToRightCDText;
                }
            }
        }
        public void SwitchNextTrack() {
            if(currentTrack < leftToRightTracks.Length - 1 && !isRadioPlaying) {
                currentTrack++;
                ChangeText(isRightToLeft);
            }
        }
        public void SwitchPreviousTrack() {
            if(currentTrack > 0 && !isRadioPlaying) {
                currentTrack--;
                ChangeText(isRightToLeft);
            }
        }
        public void SwitchFirstTrack() {
            if(currentTrack != 0 && !isRadioPlaying) {
                currentTrack = 0;
                ChangeText(isRightToLeft);
            }
        }
        public void SwitchLastTrack() {
            if(currentTrack != leftToRightTracks.Length - 1 && !isRadioPlaying) {
                currentTrack = leftToRightTracks.Length - 1;
                ChangeText(isRightToLeft);
            }
        }
    }
}
