

using System;
using System.Windows.Controls;

namespace Flow.Launcher.Plugin.SearchUnicode.Emoji
{
    /// <summary>
    ///  Interactive logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        private readonly Settings _settings;

        public SettingsControl(Settings settings)
        {
            _settings = settings;

            InitializeComponent();

            GenderTextBox.Text = _settings.Gender;
            ToneTextBox.Text = _settings.Tone;

            GenderTextBox.TextChanged += GenderTextBox_TextChanged;
            ToneTextBox.TextChanged += ToneTextBox_TextChanged;
        }

        private void GenderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _settings.Gender = GenderTextBox.Text;

        }

        private void ToneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _settings.Tone = ToneTextBox.Text;
        }

    }
}