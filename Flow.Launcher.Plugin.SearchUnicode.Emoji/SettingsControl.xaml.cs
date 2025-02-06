

using System;
using System.Windows.Controls;

namespace Flow.Launcher.Plugin.SearchUnicode.Emoji
{
    /// <summary>
    ///  Interactive logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        private Settings _settings;
        private readonly PluginInitContext _context;

        public SettingsControl(PluginInitContext context)
        {
            _context = context;
            _settings = context.API.LoadSettingJsonStorage<Settings>();

            InitializeComponent();

            GenderTextBox.Text = _settings.Gender;
            ToneTextBox.Text = _settings.Tone;

            GenderTextBox.TextChanged += GenderTextBox_TextChanged;
            ToneTextBox.TextChanged += ToneTextBox_TextChanged;
        }

        private void GenderTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _settings = _context.API.LoadSettingJsonStorage<Settings>();
            _settings.Gender = GenderTextBox.Text;
            _context.API.SaveSettingJsonStorage<Settings>();
        }

        private void ToneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _settings = _context.API.LoadSettingJsonStorage<Settings>();
            _settings.Tone = ToneTextBox.Text;
            _context.API.SaveSettingJsonStorage<Settings>();
        }

    }
}