//Copyright 2018 Samsung Electronics Co., Ltd
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TextToSpeech.Model
{
    class TextToSpeechModel
    {
        #region fields

        private static readonly string STATE_SETTINGS_LANGUAGE_KEY = "language";

        private static readonly string STATE_SETTINGS_SOUND_ON_KEY = "sound_on";

        private string _language;

        private IDictionary<string, object> _state;

        private bool _soundOn = true;

        private bool _recognitionStopped;

        private IEnumerable<Locale> _locales;

        private CancellationTokenSource _cts;


        #endregion

        #region properties

        public event EventHandler<EventArgs> RecognitionActiveStateChanged;

        public IEnumerable<string> SupportedLanguages { get; private set; }

        public bool Ready = true;

        public string Language
        {
            get => _language;
            set
            {
                _state[STATE_SETTINGS_LANGUAGE_KEY] = value;
                _language = value;
                Application.Current.SavePropertiesAsync();
            }
        }

        public bool SoundOn
        {
            get => _soundOn;
            set
            {
                _soundOn = value;
                _state[STATE_SETTINGS_SOUND_ON_KEY] = _soundOn;
                Application.Current.SavePropertiesAsync();                
            }
        }

        public bool RecognitionActive = false;

        #endregion

        #region methods

        public TextToSpeechModel(IDictionary<string, object> state)
        {
            _state = state;
        }        

        public async Task InitAsync()
        {
            _locales = await Xamarin.Essentials.TextToSpeech.GetLocalesAsync();

            List<string> list = new List<string>();
            foreach (var locale in _locales)
                list.Add(locale.Language);

            SupportedLanguages = list;

            RestoreState();
        }

        private void RestoreState()
        {
            Language = _state.ContainsKey(STATE_SETTINGS_LANGUAGE_KEY) ?
                (string)_state[STATE_SETTINGS_LANGUAGE_KEY] : "en_US";

            SoundOn = true;
        }

        public async void StartAsync(string text)
        {
            if (_recognitionStopped)
            {
                _recognitionStopped = false;
            }

            RecognitionActive = true;
            RecognitionActiveStateChanged?.Invoke(this, new EventArgs());

            _cts = new CancellationTokenSource();

            var options = new SpeechOptions();

            options.Pitch = 2.0f;
            options.Volume = 1.0f;

            var locale = _locales.FirstOrDefault();
            foreach (var item in _locales)
            {
                if (item.Language == Language)
                {
                    locale = item;
                    break;
                }
            }

            if (locale != null)
            {
                options.Locale = locale;
            }

            await Xamarin.Essentials.TextToSpeech.SpeakAsync(text, options, _cts.Token);

            RecognitionActive = false;
            RecognitionActiveStateChanged?.Invoke(this, new EventArgs());
        }

        public void Stop()
        {
            if (_cts?.IsCancellationRequested ?? true)
                return;
            _cts.Cancel();
            _recognitionStopped = true;
        }

        #endregion
    }
}
