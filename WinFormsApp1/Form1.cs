using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Windows.Forms;
using NAudio.Wave;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private WaveInEvent waveIn;
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOutEvent waveOut; //マイク出力
        private WaveOutEvent waveOutMusic; //audio file
        private AudioFileReader audioFile;
        private string selectedFolderPath;
        string[] musicFilePaths;
        private bool isPaused;

        public Form1()
        {
            InitializeComponent();
            StartUp();
        }

        private void StartUp()
        {
            // WaveOutとMp3FileReaderを初期化
            waveOutMusic = new WaveOutEvent();
        }

        private void InitializeAudio(int input, int output, int buffasize) // 起動
        {
            // マイクの設定
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = input;
            waveIn.WaveFormat = new WaveFormat(44100, 1); // モノラル
            waveIn.DataAvailable += OnDataAvailable;

            // 出力デバイスの設定
            waveOut = new WaveOutEvent();
            waveOut.DeviceNumber = output;

            // バッファサイズの設定
            int bufferSize = buffasize; // 適切なサイズを選択
            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);
            bufferedWaveProvider.BufferLength = bufferSize;
            bufferedWaveProvider.DiscardOnBufferOverflow = true;

            // バッファからデータを出力
            waveOut.Init(bufferedWaveProvider);
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            // マイクからの音声データをバッファに書き込む
            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // コンボボックス１に入力デバイスの名前を代入
            for (int i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                var deviceInfo = WaveInEvent.GetCapabilities(i);
                inputDevices.Items.Add(deviceInfo.ProductName);
            }
            inputDevices.SelectedIndex = 0;

            // コンボボックス2に出力デバイスの名前を代入
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var deviceInfo = WaveOut.GetCapabilities(i);
                outputDevices.Items.Add(deviceInfo.ProductName);
            }
            outputDevices.SelectedIndex = 0;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // フォームが閉じられるときにクリーンアップ
            if (waveIn != null && waveOut != null)
            {
                waveIn.StopRecording();
                waveOut.Stop();
            }

            if (waveOutMusic != null)
            {
                waveOutMusic.Stop();
                waveOutMusic.Dispose();
            }
        }

        private void start_Click(object sender, EventArgs e)
        {
            // stop
            if (waveIn != null && waveOut != null)
            {
                waveIn.StopRecording();
                waveOut.Stop();
                waveOut.Dispose();
            }

            int inputDeviceNumber = inputDevices.SelectedIndex;
            int outputDeviceNumber = outputDevices.SelectedIndex;
            int buffaSize = (int)Buffa.Value;

            InitializeAudio(inputDeviceNumber, outputDeviceNumber, buffaSize);
            waveIn.StartRecording();
            waveOut.Play();

            // label
            Status.ForeColor = Color.Lime;
            Status.Text = "稼働中";
        }

        private void stop_Click(object sender, EventArgs e)
        {
            // stop
            if (waveIn != null)
            {
                waveIn.StopRecording();
            }

            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }

            //label
            Status.ForeColor = Color.Red;
            Status.Text = "停止中";

        }

        private void play_Click(object sender, EventArgs e)
        {
            if (MusicFileCombo.Items.Count != 0)
            {
                // MP3 ファイルのパス
                string AudioFilePath = musicFilePaths[MusicFileCombo.SelectedIndex];

                // 出力デバイスの設定
                waveOutMusic.DeviceNumber = outputDevices.SelectedIndex;

                if (waveOutMusic != null && waveOutMusic.PlaybackState != PlaybackState.Playing && isPaused == false)
                {
                    audioFile = new AudioFileReader(AudioFilePath);
                    audioFile.Volume = (float)Volume.Value / 100.0f;
                    waveOutMusic.Init(audioFile);
                    waveOutMusic.Play();

                    // TrackBarの最大値をMP3の長さに設定
                    mediaPos.Maximum = (int)audioFile.TotalTime.TotalMilliseconds;
                    mediaPos.Value = 0;

                    //timer 設定
                    currentTime.Start();
                    lastTime.Start();

                }
                else
                {
                    waveOutMusic.Play();
                    isPaused = false;
                }
            }
        }

        private void stopSound_Click(object sender, EventArgs e)
        {
            if (waveOutMusic != null)
            {
                waveOutMusic.Stop();
                waveOutMusic.Dispose();
                mediaPos.Value = 0;
                currentTime.Stop();
                lastTime.Stop();
                isPaused = false;
            }
        }

        private void OpenFolder_Click(object sender, EventArgs e)
        {
            // FolderBrowserDialog のインスタンスを作成
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // ダイアログの説明文を設定
                folderBrowserDialog.Description = "フォルダを選択してください";

                // ダイアログを表示
                DialogResult result = folderBrowserDialog.ShowDialog();

                // ユーザーが OK ボタンをクリックした場合
                if (result == DialogResult.OK)
                {
                    // 選択されたフォルダのパスを取得
                    selectedFolderPath = folderBrowserDialog.SelectedPath;

                    // フォルダ内の音楽ファイルを取得
                    string[] mp3Files = Directory.GetFiles(selectedFolderPath, "*.mp3");
                    string[] wavFiles = Directory.GetFiles(selectedFolderPath, "*.wav");
                    string[] musicFiles = mp3Files.Concat(wavFiles).ToArray();

                    musicFilePaths = musicFiles;


                    // textboxに取得したフォルダのパスを設定
                    Paths.Text = selectedFolderPath;

                    // リストボックスをクリア
                    MusicFileList.Items.Clear();

                    // コンボボックスをクリア
                    MusicFileCombo.Items.Clear();

                    // 取得した音楽ファイルをリストボックスに追加
                    foreach (string musicFile in musicFiles)
                    {
                        // ファイル名だけ抽出して追加
                        MusicFileList.Items.Add(Path.GetFileName(musicFile));
                        MusicFileCombo.Items.Add(Path.GetFileName(musicFile));
                    }

                    if (MusicFileCombo.Items.Count != 0)
                    {
                        MusicFileCombo.SelectedIndex = 0;
                        MusicFileCombo.MaxDropDownItems = musicFiles.Length;
                    }
                }
            }
        }

        private void Volume_Scroll(object sender, EventArgs e)
        {
            if (audioFile != null)
            {
                // トラックバーの値を取得して音量に適用
                audioFile.Volume = (float)Volume.Value / 100.0f;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (waveOutMusic.PlaybackState == PlaybackState.Playing)
            {
                isPaused = true;
                waveOutMusic.Pause();
            }
        }

        private void mediaPos_Scroll(object sender, EventArgs e)
        {
            if (waveOutMusic.PlaybackState == PlaybackState.Playing)
            {
                // TrackBarの値に応じて再生位置を変更
                audioFile.CurrentTime = TimeSpan.FromMilliseconds(mediaPos.Value);
            }
            else
            {
                mediaPos.Value = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (waveOutMusic.PlaybackState == PlaybackState.Playing || isPaused == true)
            {
                if ((int)audioFile.CurrentTime.TotalMilliseconds < mediaPos.Maximum)
                {
                    // TrackBarの値を再生位置に合わせる
                    mediaPos.Value = (int)audioFile.CurrentTime.TotalMilliseconds;
                }

            }
            else
            {
                mediaPos.Value = 0;
            }

        }

        private void lastTime_Tick(object sender, EventArgs e)
        {
            int totalTime = (int)audioFile.TotalTime.TotalSeconds - (int)audioFile.CurrentTime.TotalSeconds;
            int totalHour = totalTime / 3600;
            int totalMinute = totalTime / 60;
            int totalSeocnd = totalTime - totalHour * 3600 - totalMinute * 60;
            time.Text = $"残り{totalHour}時間{totalMinute}分{totalSeocnd}秒";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedFolderPath != null)
            {
                // フォルダ内の音楽ファイルを取得
                string[] mp3Files = Directory.GetFiles(selectedFolderPath, "*.mp3");
                string[] wavFiles = Directory.GetFiles(selectedFolderPath, "*.wav");
                string[] musicFiles = mp3Files.Concat(wavFiles).ToArray();

                musicFilePaths = musicFiles;


                // textboxに取得したフォルダのパスを設定
                Paths.Text = selectedFolderPath;

                // リストボックスをクリア
                MusicFileList.Items.Clear();

                // コンボボックスをクリア
                MusicFileCombo.Items.Clear();

                // 取得した音楽ファイルをリストボックスに追加
                foreach (string musicFile in musicFiles)
                {
                    // ファイル名だけ抽出して追加
                    MusicFileList.Items.Add(Path.GetFileName(musicFile));
                    MusicFileCombo.Items.Add(Path.GetFileName(musicFile));
                }

                if (MusicFileCombo.Items.Count != 0)
                {
                    MusicFileCombo.SelectedIndex = 0;
                    MusicFileCombo.MaxDropDownItems = musicFiles.Length;
                }
            }
        }

        private void MusicFileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (waveOutMusic != null)
            {
                waveOutMusic.Stop();
                waveOutMusic.Dispose();
                currentTime.Stop();
                lastTime.Stop();
                mediaPos.Value = 0;
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ProcessStartInfo pi = new ProcessStartInfo()
            {
                FileName = e.LinkText,
                UseShellExecute = true,
            };
            Process.Start(pi);
        }
    }
}