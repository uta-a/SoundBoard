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
        private WaveOutEvent waveOut; //�}�C�N�o��
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
            // WaveOut��Mp3FileReader��������
            waveOutMusic = new WaveOutEvent();
        }

        private void InitializeAudio(int input, int output, int buffasize) // �N��
        {
            // �}�C�N�̐ݒ�
            waveIn = new WaveInEvent();
            waveIn.DeviceNumber = input;
            waveIn.WaveFormat = new WaveFormat(44100, 1); // ���m����
            waveIn.DataAvailable += OnDataAvailable;

            // �o�̓f�o�C�X�̐ݒ�
            waveOut = new WaveOutEvent();
            waveOut.DeviceNumber = output;

            // �o�b�t�@�T�C�Y�̐ݒ�
            int bufferSize = buffasize; // �K�؂ȃT�C�Y��I��
            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);
            bufferedWaveProvider.BufferLength = bufferSize;
            bufferedWaveProvider.DiscardOnBufferOverflow = true;

            // �o�b�t�@����f�[�^���o��
            waveOut.Init(bufferedWaveProvider);
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            // �}�C�N����̉����f�[�^���o�b�t�@�ɏ�������
            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // �R���{�{�b�N�X�P�ɓ��̓f�o�C�X�̖��O����
            for (int i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                var deviceInfo = WaveInEvent.GetCapabilities(i);
                inputDevices.Items.Add(deviceInfo.ProductName);
            }
            inputDevices.SelectedIndex = 0;

            // �R���{�{�b�N�X2�ɏo�̓f�o�C�X�̖��O����
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var deviceInfo = WaveOut.GetCapabilities(i);
                outputDevices.Items.Add(deviceInfo.ProductName);
            }
            outputDevices.SelectedIndex = 0;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // �t�H�[����������Ƃ��ɃN���[���A�b�v
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
            Status.Text = "�ғ���";
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
            Status.Text = "��~��";

        }

        private void play_Click(object sender, EventArgs e)
        {
            if (MusicFileCombo.Items.Count != 0)
            {
                // MP3 �t�@�C���̃p�X
                string AudioFilePath = musicFilePaths[MusicFileCombo.SelectedIndex];

                // �o�̓f�o�C�X�̐ݒ�
                waveOutMusic.DeviceNumber = outputDevices.SelectedIndex;

                if (waveOutMusic != null && waveOutMusic.PlaybackState != PlaybackState.Playing && isPaused == false)
                {
                    audioFile = new AudioFileReader(AudioFilePath);
                    audioFile.Volume = (float)Volume.Value / 100.0f;
                    waveOutMusic.Init(audioFile);
                    waveOutMusic.Play();

                    // TrackBar�̍ő�l��MP3�̒����ɐݒ�
                    mediaPos.Maximum = (int)audioFile.TotalTime.TotalMilliseconds;
                    mediaPos.Value = 0;

                    //timer �ݒ�
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
            // FolderBrowserDialog �̃C���X�^���X���쐬
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // �_�C�A���O�̐�������ݒ�
                folderBrowserDialog.Description = "�t�H���_��I�����Ă�������";

                // �_�C�A���O��\��
                DialogResult result = folderBrowserDialog.ShowDialog();

                // ���[�U�[�� OK �{�^�����N���b�N�����ꍇ
                if (result == DialogResult.OK)
                {
                    // �I�����ꂽ�t�H���_�̃p�X���擾
                    selectedFolderPath = folderBrowserDialog.SelectedPath;

                    // �t�H���_���̉��y�t�@�C�����擾
                    string[] mp3Files = Directory.GetFiles(selectedFolderPath, "*.mp3");
                    string[] wavFiles = Directory.GetFiles(selectedFolderPath, "*.wav");
                    string[] musicFiles = mp3Files.Concat(wavFiles).ToArray();

                    musicFilePaths = musicFiles;


                    // textbox�Ɏ擾�����t�H���_�̃p�X��ݒ�
                    Paths.Text = selectedFolderPath;

                    // ���X�g�{�b�N�X���N���A
                    MusicFileList.Items.Clear();

                    // �R���{�{�b�N�X���N���A
                    MusicFileCombo.Items.Clear();

                    // �擾�������y�t�@�C�������X�g�{�b�N�X�ɒǉ�
                    foreach (string musicFile in musicFiles)
                    {
                        // �t�@�C�����������o���Ēǉ�
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
                // �g���b�N�o�[�̒l���擾���ĉ��ʂɓK�p
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
                // TrackBar�̒l�ɉ����čĐ��ʒu��ύX
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
                    // TrackBar�̒l���Đ��ʒu�ɍ��킹��
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
            time.Text = $"�c��{totalHour}����{totalMinute}��{totalSeocnd}�b";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedFolderPath != null)
            {
                // �t�H���_���̉��y�t�@�C�����擾
                string[] mp3Files = Directory.GetFiles(selectedFolderPath, "*.mp3");
                string[] wavFiles = Directory.GetFiles(selectedFolderPath, "*.wav");
                string[] musicFiles = mp3Files.Concat(wavFiles).ToArray();

                musicFilePaths = musicFiles;


                // textbox�Ɏ擾�����t�H���_�̃p�X��ݒ�
                Paths.Text = selectedFolderPath;

                // ���X�g�{�b�N�X���N���A
                MusicFileList.Items.Clear();

                // �R���{�{�b�N�X���N���A
                MusicFileCombo.Items.Clear();

                // �擾�������y�t�@�C�������X�g�{�b�N�X�ɒǉ�
                foreach (string musicFile in musicFiles)
                {
                    // �t�@�C�����������o���Ēǉ�
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