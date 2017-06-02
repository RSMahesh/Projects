using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace UserActivityLogger
{
    public class KeyProcessor : IKeyProcessor
    {
        const string UnProcssedKey = "UnProcssedKey";

        List<SpecificKeysProcessor> _specificKeysProcessors = new List<SpecificKeysProcessor>();

        public KeyProcessor()
        {
            _specificKeysProcessors.Add(new CapsLockKeyProcessor());
            _specificKeysProcessors.Add(new SpaceBarKeyProcessor());
            _specificKeysProcessors.Add(new NumberKeyProcessor());
            _specificKeysProcessors.Add(new FunctionKeyProcessor());
            _specificKeysProcessors.Add(new LetterKeyProcessor());
            _specificKeysProcessors.Add(new PunctuationKeyProcessor());
            _specificKeysProcessors.Add(new EnterDeleteBackKeyProcessor());
        }

        const string KEY = "KEY";
        public ProcessedKeyData ProcessKeys(string keyBuffer)
        {
            var keys = keyBuffer.Split(new string[] { KEY }, StringSplitOptions.None);

            var processedData = new StringBuilder();
            var unProcessedData = new StringBuilder();

            for (var i = 1; i < keys.Length; i++)
            {
                var ch = ProcessLogedKey(keys[i]);

                if (ch == UnProcssedKey)
                {
                    //TODO: have to think how we make it more meaingful
                    // as keeping data seperte makes hard to understand whether 
                    // user has pressed some meta key
                    // unProcessedData.Append(keys[i]);
                    processedData.Append(ch);
                }
                else if (!string.IsNullOrEmpty(ch))
                {
                    processedData.Append(ch);
                }
            }

            return new ProcessedKeyData(processedData.ToString(), unProcessedData.ToString());
        }

        private string ProcessLogedKey(string loggedKey)
        {
            foreach (var keyProcessor in _specificKeysProcessors)
            {
                if (keyProcessor.CanProcess(loggedKey))
                {
                    return keyProcessor.ProcessKey(loggedKey);
                }
            }

            return UnProcssedKey;

        }
    }
}