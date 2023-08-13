using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TradeHelper.Misc
{
    public static class LogParser
    {
        private static long _lastPos;
        private static FileSystemWatcher _watcher;
        private static string _tempPath;
        private static CancellationTokenSource _cts;
        private static Task _forceUpdateFileTask;

        public static void InitWatcher()
        {
            _watcher = new FileSystemWatcher(Path.GetDirectoryName(Settings.LogfilePath),
                Path.GetFileName(Settings.LogfilePath));

            _watcher.NotifyFilter = NotifyFilters.Attributes
                                    | NotifyFilters.CreationTime
                                    | NotifyFilters.LastAccess
                                    | NotifyFilters.LastWrite
                                    | NotifyFilters.Size;

            _watcher.Changed += OnChanged;
            _watcher.Error += OnError;

            _watcher.EnableRaisingEvents = true;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }

            CheckChangedLines();
        }

        private static void OnError(object sender, ErrorEventArgs ex)
        {
            DefaultLogger.Error(ex.GetException().Message, ex.GetException());
        }

        public static void InitLogReader()
        {
            _tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            File.Copy(Settings.LogfilePath, _tempPath, true);

            DefaultLogger.Info($"Logs successfully copied in temp file {_tempPath}.");

            using (var freader = new FileStream(_tempPath, FileMode.Open))
            {
                _lastPos = freader.Length;
            }
        }

        public static void StartForceUpdateFile()
        {
            if (_cts == null || _cts.IsCancellationRequested)
            {
                _cts = new CancellationTokenSource();
            }

            _forceUpdateFileTask = TryForceUpdateFileTask();
        }

        public static void StopForceUpdateFile()
        {
            if (_forceUpdateFileTask == null || _cts == null)
            {
                return;
            }

            if (!_forceUpdateFileTask.IsCompleted)
            {
                _cts.Cancel();
            }

            if (_forceUpdateFileTask != null && _forceUpdateFileTask.IsCompleted)
            {
                _forceUpdateFileTask = null;
            }

            if (File.Exists(_tempPath))
            {
                File.Delete(_tempPath);
            }
        }

        private static void CheckChangedLines()
        {
            Thread.Sleep(500);
            Settings.SetLanguage();

            var length = new FileInfo(Settings.LogfilePath).Length;

            if (length != _lastPos)
            {
                File.Copy(Settings.LogfilePath, _tempPath, true);

                using (var freader = File.Open(_tempPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    freader.Seek(_lastPos, SeekOrigin.Begin);

                    using (var reader = new StreamReader(freader))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();

                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                var regexResult = Regex.IsMatch(line,
                                                      Resources.ParserRegex.pmMessageStarter +
                                                      Resources.ParserRegex.poeTradeRegex) ||
                                                  Regex.IsMatch(line,
                                                      Resources.ParserRegex.pmMessageStarter +
                                                      Resources.ParserRegex.poeTradeUnpricedRegex) ||
                                                  Regex.IsMatch(line,
                                                      Resources.ParserRegex.pmMessageStarter +
                                                      Resources.ParserRegex.poeTradeCurrencyRegex);

                                DefaultLogger.Info($"Line is not null: {line}" + Environment.NewLine +
                                                   $"RegexResult: {regexResult}");

                                if (!Settings.AcceptAllMessages && regexResult)
                                {
                                    GetPmMessageAndSentToTg(line);
                                }
                                else if (Settings.AcceptAllMessages &&
                                         Regex.IsMatch(line, Resources.ParserRegex.pmMessageStarter))
                                {
                                    GetPmMessageAndSentToTg(line);
                                }
                            }
                        }

                        _lastPos = freader.Position;
                    }
                }
            }
        }

        private static void GetPmMessageAndSentToTg(string message)
        {
            DefaultLogger.Info("Before sending.");
            var stopLine = Regex.Match(message, Resources.ParserRegex.pmMessageStarter).Value;
            var indexOfMessage = message.IndexOf(stopLine, StringComparison.Ordinal) + stopLine.Length;

            var formattedMessage = message.Substring(indexOfMessage).Trim();
            TelegramWorker.SendMessage(formattedMessage);
            DefaultLogger.Info($"Message sent: {formattedMessage}");
        }

        private static async Task TryForceUpdateFileTask()
        {
            await Task.Run(() => TryForceUpdateFile(_cts.Token), _cts.Token);
        }

        private static void TryForceUpdateFile(CancellationToken token)
        {
            while (true)
            {
                try
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }

                    Thread.Sleep(Settings.UpdateTime ?? 1000);

                    var now = DateTime.Now;

                    var lat = File.GetLastAccessTime(Settings.LogfilePath);
                    File.SetLastAccessTime(Settings.LogfilePath, now);

                    DefaultLogger.Info($"Successfully updated last access time: {Settings.LogfilePath} old:\"{lat}\" new:\"{now}\" {File.GetAttributes(Settings.LogfilePath)}");
                }
                catch (Exception ex)
                {
                    DefaultLogger.Error($"Unsuccessfully to update last access time: {ex.Message}", ex);
                }
            }
        }
    }
}
