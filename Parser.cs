using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace BitBox
{
    public class Parser
    {
        private Data data;
        private List<Plugin> plugins;
        private List<string> Existingplugins;

        public delegate void GetParsedData(ViewingData data);
        private GetParsedData parsedDataViewer;

        public Parser(GetParsedData parsedDataViewer)
        {
            plugins = new List<Plugin>();
            Existingplugins = new List<string>();
            this.parsedDataViewer = parsedDataViewer;
        }

        public Data Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                Parse();
            }
        }

        public void AddPlugin(Plugin plugin)
        {
            plugins.Add(plugin);
            Parse();
        }

        public void Update()
        {
            Parse();
        }

        private void Parse()
        {
            foreach(Plugin p in plugins)
            {
                data = p.Parse(data);
            }
            parsedDataViewer(new ViewingData(data));
        }

        public void UpdateExistingPlugins(string plugnsDirectory)
        {
            foreach (string file in Directory.GetFiles(plugnsDirectory))
            {
                if (!Existingplugins.Contains(file.Substring(file.LastIndexOf("\\") + 1, file.Length - file.LastIndexOf("\\") - 4)))
                    if (IsPlugin(file))
                        Existingplugins.Add(file.Substring(file.LastIndexOf("\\")+1, file.Length - file.LastIndexOf("\\") - 4));
            }
        }

        private bool IsPlugin(string filePath) 
        {
            if (filePath.LastIndexOf(".cs") != filePath.Length - 3)
                return false;
            string data = File.ReadAllText(filePath);
            if (!data.Contains(": Plugin"))
                return false;
            return true;
        }

        public List<string> SearchInExistingPlugin(string name)
        {
            List<string> containingPlugins = new List<string>();
            foreach(string pluginName in Existingplugins)
            {
                if (pluginName.StartsWith(name,StringComparison.OrdinalIgnoreCase))
                    containingPlugins.Add(pluginName);
            }
            return containingPlugins;
        }

    }
}
