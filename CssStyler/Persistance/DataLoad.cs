using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CssStyler.Persistance
{
    class DataLoad
    {
        private string _path = "teszt.html";
        public event EventHandler<StringEventArg> FileLoaded;
        public DataLoad()
        {

        }
        
        private void OnFileLoaded(string str)
        {
            FileLoaded?.Invoke(this, new StringEventArg(str));
        }

        public void SetPath(string msg)
        {
            _path = msg;
        }

        public void LoadFile()
        {
            Task o = new Task(async () =>
            {
                StreamReader file = new StreamReader(_path);
                string str = await file.ReadToEndAsync();
                OnFileLoaded(str);
            });
            o.Start();
        }

    }
}
