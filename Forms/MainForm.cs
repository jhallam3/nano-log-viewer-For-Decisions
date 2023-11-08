using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NanoLogViewer.Datatype;
using NanoLogViewer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NanoLogViewer.Forms
{
	public partial class MainForm : Form
	{
		readonly SynchronizationContext synchronizationContext;
		readonly BackgroundThread backgroundThread = new BackgroundThread();

        string sourcesIniFileName => Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\sources.ini";
        string columnsIniFileName => Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\columns.ini";
        string filtersIniFileName => Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\filters.ini";

        readonly Dictionary<string, int> columnWidths = new Dictionary<string, int>();
        readonly Dictionary<string, int> columnIndexes = new Dictionary<string, int>();

        bool noRemeberColumns = false;

        private List<JObject> allLoadedObjects = new List<JObject>();

        List<ComboBox> freezed = new List<ComboBox>();

        public MainForm()
		{
			synchronizationContext = SynchronizationContext.Current;

			InitializeComponent();

            wbDetails.Navigate("about:blank");

            if (File.Exists(sourcesIniFileName))
            {
                foreach (var source in File.ReadAllLines(sourcesIniFileName))
                {
                    if (!string.IsNullOrWhiteSpace(source)) cbSource.Items.Add(source);
                }
            }

            if (File.Exists(columnsIniFileName))
            {
                var i = 0;
                foreach (var column in File.ReadAllLines(columnsIniFileName))
                {
                    if (!string.IsNullOrWhiteSpace(column))
                    {
                        columnWidths[column.Split(':')[0]] = int.Parse(column.Split(':')[1]);
                        columnIndexes[column.Split(':')[0]] = i; i++;
                    }
                }
            }

            if (File.Exists(filtersIniFileName))
            {
                foreach (var filter in File.ReadAllLines(filtersIniFileName))
                {
                    if (!string.IsNullOrWhiteSpace(filter)) cbFilter.Items.Add(filter);
                }
            }

            var args = Environment.GetCommandLineArgs();
			if (args.Length == 2)
			{
				cbSource.Text = args[1];
				btUpdate_Click(null, null);
			}
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            var sources = new List<string>();
            for (var i = 0; i < Math.Min(10, cbSource.Items.Count); i++)
            {
                sources.Add(cbSource.GetItemText(cbSource.Items[i]));
            }
            File.WriteAllLines(sourcesIniFileName, sources);

            rememberListState();

            var columns = new List<string>();
            foreach (var key in columnIndexes.OrderBy(x => x.Value).Select(x => x.Key))
            {
                columns.Add(key + ":" + columnWidths[key]);
            }
            File.WriteAllLines(columnsIniFileName, columns);
            
            var filters = new List<string>();
            for (var i = 0; i < Math.Min(10, cbFilter.Items.Count); i++)
            {
                filters.Add(cbFilter.GetItemText(cbFilter.Items[i]));
            }
            File.WriteAllLines(filtersIniFileName, filters);
        }

        private void runInFormThread(Action work)
		{
			synchronizationContext.Post(_ => work(), null);
		}

		private void btSelectSourceFile_Click(object sender, EventArgs e)
		{
			if (openLogFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				cbSource.Text = openLogFileDialog.FileName;
				btUpdate_Click(null, null);
			}
		}

        private void btUpdate_Click(object sender, EventArgs e)
        {
            var urlHistory = new List<string>();
            foreach (var sourceItem in cbSource.Items)
            {
                urlHistory.Add(cbSource.GetItemText(sourceItem));
            }

            updateInner(cbSource.Text.Trim(), urlHistory);
        }

        private void updateInner(string uri, List<string> urlHistory)
		{
            btUpdate.Enabled = false;
            var isLoadTail = cbTailMB.Checked;

            if (uri != "")
			{
                if (uri.StartsWith("http://") || uri.StartsWith("https://"))
				{
                    Task.Run(() =>
                    {
                        try
                        {
                            var size = isLoadTail ? HttpTools.getSize(uri) : null;
                            var text = HttpTools.download(uri, size != null ? (int?)Math.Max(0, size.Value - 1024*1024) : null);
                            text = ConvertDecisionsLogsToNanoLog(text);
                            runInFormThread(() =>
                            {
                                addItemToComboBox(cbSource, uri);
                                
                                allLoadedObjects = parse(text);
                                fillListView(allLoadedObjects);
                                btUpdate.Enabled = true;
                                cbSource.Text = uri;
                            });
                        }
                        catch (Exception ee)
                        {
                            if (ee is WebException)
                            {
                                var response = ((WebException)ee).Response as HttpWebResponse;
                                if (response?.StatusCode == HttpStatusCode.Unauthorized)
                                {
                                    var parsedUri = new Uri(uri);
                                    if (parsedUri.UserInfo == "")
                                    {
                                        foreach (var h in urlHistory)
                                        {
                                            if (h.StartsWith("http://") || h.StartsWith("https://"))
                                            {
                                                var pu = new Uri(h);
                                                if (pu.Host == parsedUri.Host && pu.UserInfo != "")
                                                {
                                                    runInFormThread(() => updateInner(parsedUri.Scheme + "://" + pu.UserInfo + "@" + parsedUri.Host + parsedUri.PathAndQuery, new List<string>()));
                                                    return;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            runInFormThread(() =>
                            {
                                MessageBox.Show(this, ee.Message, "Downloading error", MessageBoxButtons.OK);
                                btUpdate.Enabled = true;
                            });
                        }
                    });
                }
                else
				{
                    if (File.Exists(uri))
                    {
                        addItemToComboBox(cbSource, uri);

                        allLoadedObjects = parse(File.ReadAllText(uri));
                        fillListView(allLoadedObjects);
                        btUpdate.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public string ConvertDecisionsLogsToNanoLog(string contents)
        {
            var AsDecisionsJson = JsonConvert.DeserializeObject<DecisionsDone>(contents);
            
            var result = AsDecisionsJson.Done.Rows.ToString();
            //foreach (var i in AsDecisionsJson.Done.Rows)
            //{
            //    var msg = "";
            //    if (!string.IsNullOrWhiteSpace(i.Message))
            //    {
            //        msg = i.Message.Replace("\r\n", " ");
            //        msg = msg.Replace("{", " ");
            //        msg = msg.Replace("}", " ");
            //        msg = msg.Replace("[", " ");
            //        msg = msg.Replace("]", " ");
            //        msg = msg.Replace("\"", " ");
            //    }


            //    result = result + "{\"Category\": " + "\"" + i.Category + "\", \"Level\": " + "\"" + i.Level + "\", \"Timestamp\": " + "\"" + i.Timestamp + "\", \"Message\":" + "\"" + msg + "\", \"entity_id\":" + "\"" + i.entity_id + "\", \"entity_type_name\":" + "\"" + i.entity_type_name + "\"}" + "\r\n";



            //}
            return result;
        }

        List<JObject> parse(string text)
        {
            var r = new List<JObject>();
            
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            
            var lines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var json = line.Trim();
                if (!json.StartsWith("{")) continue;

                var obj = JObject.Parse(json);
                r.Add(obj);
            }

            return r;
        }

        void fillListView(IEnumerable<JObject> allObjects)
        {
            noRemeberColumns = true;

            wbDetails.DocumentText = "";

            lvLogLines.Clear();

            var objectsToDisplay = new List<JObject>();
            foreach (var obj in allObjects)
            {
                if (!isDisplayObject(obj)) continue;
                
                objectsToDisplay.Add(obj);
                
                foreach (var prop in obj)
                {
                    if (!lvLogLines.Columns.ContainsKey(prop.Key))
                    {
                        lvLogLines.Columns.Add(prop.Key, prop.Key);
                    }
                }
            }

            foreach (var obj in objectsToDisplay)
            {
                var subItems = new List<string>();
                foreach (ColumnHeader col in lvLogLines.Columns)
                {
                    subItems.Add(jtokenToString(obj[col.Name]));
                }

                var item = new ListViewItem(subItems.ToArray());
                item.Tag = obj;
                lvLogLines.Items.Add(item);
            }

            var columns = new List<ColumnHeader>();
            foreach (ColumnHeader col in lvLogLines.Columns) columns.Add(col);

            var sortedColumns = columns.Select(col => Tuple.Create(col, columnWidths.ContainsKey(col.Name) ? columnIndexes[col.Name] : 1000))
                                       .OrderBy(x => x.Item2).ThenBy(x => x.Item1.Name)
                                       .ToArray();

            for (var i = 0; i < sortedColumns.Length; i++)
            {
                var col = sortedColumns[i].Item1;
                if (columnWidths.ContainsKey(col.Name))
                {
                    col.Width = columnWidths[col.Name];
                }
                else
                {
                    columnWidths[col.Name] = col.Width;
                    columnIndexes[col.Name] = sortedColumns[i].Item2;
                }
                col.DisplayIndex = i;
            }

            if (lvLogLines.Items.Count > 0)
            {
                lvLogLines.EnsureVisible(lvLogLines.Items.Count - 1);
                lvLogLines.Items[lvLogLines.Items.Count - 1].Selected = true;
            }

            noRemeberColumns = false;

            lvLogLines.Refresh();
        }
        
        private bool isDisplayObject(JObject obj)
        {
            var s = JsonConvert.SerializeObject(obj);
            return s.Contains(cbFilter.Text);
        }

        private void lvLogLines_SelectedIndexChanged(object sender, EventArgs e)
		{
			var item = lvLogLines.SelectedItems.Count == 1 ? lvLogLines.SelectedItems[0] : null;
			wbDetails.DocumentText = item != null ? "<pre>" + jobjectToString((JObject)item.Tag) + "</pre>" : "";
		}

		string jobjectToString(JObject obj)
		{
			var s = "";
			foreach (var prop in obj)
			{
                if (prop.Value is JObject)
                {
                    s += prop.Key + ":\n\t" + jobjectToString((JObject)prop.Value).Replace("\n", "\n\t") + "\n";
                }
                else
                {
                    var value = jtokenToString(prop.Value).Replace("\r\n", "\n").Replace("\r", "\n").Trim();
                    s += prop.Key + ": " + (value.Contains("\n") ? "\n\t" + value.Replace("\n", "\n\t") : value) + "\n";
                }
            }
			return s.Trim();
		}

        private string jtokenToString(JToken value)
        {
            if (value == null) return "";

            if (value.Type == JTokenType.Date)
            {
                var dt = ((DateTime)value).ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return value.ToString() ?? "";
        }

        private void LvLogLines_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            rememberListState();
        }

        private void LvLogLines_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            rememberListState();
        }

        private void rememberListState()
        {
            if (noRemeberColumns) return;

            foreach (ColumnHeader col in lvLogLines.Columns)
            {
                columnWidths[col.Name] = col.Width;
                columnIndexes[col.Name] = col.DisplayIndex;
            }
        }

        private void CbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (freezed.Contains(cbSource)) return;

            btUpdate_Click(null, null);
        }

        private void btClearFilter_Click(object sender, EventArgs e)
        {
            cbFilter.Text = "";
            fillListView(allLoadedObjects);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (freezed.Contains(cbFilter)) return;

            fillListView(allLoadedObjects);
        }

        private void cbFilter_TextUpdate(object sender, EventArgs e)
        {
            if (freezed.Contains(cbFilter)) return;
            
            timerUpdateAfterFilterChange.Stop();
            timerUpdateAfterFilterChange.Start();
        }

        private void timerUpdateAfterFilterChange_Tick(object sender, EventArgs e)
        {
            fillListView(allLoadedObjects);

            if (cbFilter.Text != "")
            {
                addItemToComboBox(cbFilter, cbFilter.Text);
            }
            
            timerUpdateAfterFilterChange.Stop();
        }

        private void addItemToComboBox(ComboBox cb, string item)
        {
            if (cb.Items.Contains(item)) return;
            
            freezed.Add(cb);
            cb.Items.Insert(0, item);
            freezed.Remove(cb);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
