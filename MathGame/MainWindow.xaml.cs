using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MathGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int upper_bound = 100;
        int sec_count = 0, min_count = 0;
        int time_user = 5, user_primer = 25, count_game;
        private SoundPlayer MusicTrue;
        private SoundPlayer MusicFalse;
        GeneratorExpression ge;
        Expression exp;
        StatisticClass stat;
        Class_Attempt attempt;
        string password = "маруся";

        MagicSquareClass magic;

        public MainWindow()
        {
            InitializeComponent();

            ge = new GeneratorExpression(10, upper_bound);
            MusicFalse = new SoundPlayer();
            MusicTrue = new SoundPlayer();
            attempt = new Class_Attempt();
            stat = new StatisticClass();

            magic = new MagicSquareClass();
            magic_true_inLabel = 0;
            magic_false_inLabel = 0;
        }

        private void FaierImage_MouseMove(object sender, MouseEventArgs e)
        {
            FaierImage.Height = 225;
            FaierImage.Width = 210;
            label1.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Hidden;
        }

        private void FaierImage_MouseLeave(object sender, MouseEventArgs e)
        {
            FaierImage.Height = 215;
            FaierImage.Width = 195;
            label1.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Visible;
        }

        private void FaierImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridFaier.Visibility = Visibility.Visible;

            fon.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
        }
        int time_user_magic = 5;
        System.Windows.Threading.DispatcherTimer timer_magic = new System.Windows.Threading.DispatcherTimer();
        private void timer_magicTick(object sender, EventArgs e)
        {
            string sec, min;
            sec_count++;
            if (sec_count > 59)
            {
                min_count++;
                sec_count = 0;
            }
            sec = sec_count > 9 ? sec_count.ToString() : "0" + sec_count;
            min = min_count > 9 ? min_count.ToString() : "0" + min_count;
            TimeLabel2.Content = min + ":" + sec;
            if (min_count == time_user_magic)
            {
                timer_magic.Stop();
                timer_magic = new System.Windows.Threading.DispatcherTimer();
                U = magic_true_inLabel;
                U = U / (min_count * 60 + sec_count);
                U = U * 60;
                GridGameFinish.Visibility = Visibility.Visible;
                CountPrimerItog.Content = String.Format("Вы решили квадратов: {0}", magic_true_inLabel);
                CountTimeItog.Content = String.Format("За время: {0}:{1}", min, sec);
                CountUItog.Content = String.Format("Ваша скорость: {0:#.##}", U);
                CountGameToday.Content = String.Format("Попытка за сегодня: {0}", attempt.Attempt(@"MSAttempt.txt"));
                stat.Statistic("MagStat.txt", String.Format("{0} \t {1}", U, DateTime.Now));
                magic_false_inLabel = 0;
                magic_true_inLabel = 0;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.Start();
            GridFaier.Visibility = Visibility.Hidden;
            GridDedus.Visibility = Visibility.Hidden;

            ExitImage.Visibility = Visibility.Hidden;
            ChetForm.Visibility = Visibility.Hidden;
            PrimerLabel.Visibility = Visibility.Hidden;
            OtvetTextBox.Visibility = Visibility.Hidden;
            TimeLabel.Visibility = Visibility.Hidden;
            CancelImage.Visibility = Visibility.Hidden;
            SkipImage.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            label4.Visibility = Visibility.Hidden;
            label4_Copy.Visibility = Visibility.Hidden;
            skiplabel.Visibility = Visibility.Hidden;
            falselabel.Visibility = Visibility.Hidden;
            truelabel.Visibility = Visibility.Hidden;
            HelpFaierImage.Visibility = Visibility.Hidden;
            Settingimage.Visibility = Visibility.Hidden;
            MagImage.Visibility = Visibility.Hidden;
            StatImage.Visibility = Visibility.Hidden;

            checkBoxTimeSetting.Checked += checkBoxTimeSetting_Checked;

            GridGameFinish.Visibility = Visibility.Hidden;
            GridMagic.Visibility = Visibility.Hidden;
            GridPassword.Visibility = Visibility.Hidden;
            GridSetting.Visibility = Visibility.Hidden;
            GridRecord.Visibility = Visibility.Hidden;

            ExpressionRadioButton.IsChecked = true;
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (label2.Visibility == Visibility.Visible)
                if (label2.FontSize == 36)
                {
                    label2.FontSize = 42;
                    label2.Margin = new Thickness(156, 425, -444, -391);
                }
                else
                {
                    label2.FontSize = 36;
                    label2.Margin = new Thickness(185, 435, -481, -401);
                }
            if (fon_Copy.Visibility == Visibility.Visible)
                if (label2_Copy.FontSize == 36)
                {
                    label2_Copy.FontSize = 42;
                    label2_Copy.Margin = new Thickness(156, 425, -444, -391);
                }
                else
                {
                    label2_Copy.FontSize = 36;
                    label2_Copy.Margin = new Thickness(185, 435, -481, -401);
                }
        }
        double U = 0;
        System.Windows.Threading.DispatcherTimer timer_primer = new System.Windows.Threading.DispatcherTimer();
        private void timer_primerTick(object sender, EventArgs e)
        {
            string sec, min;
            sec_count++;
            if (sec_count > 59)
            {
                min_count++;
                sec_count = 0;
            }
            sec = sec_count > 9 ? sec_count.ToString() : "0" + sec_count;
            min = min_count > 9 ? min_count.ToString() : "0" + min_count;
            TimeLabel.Content = min + ":" + sec;
            if (checkBoxTimeSetting.IsChecked.Value)
            {
                if (min_count == time_user)
                {
                    timer_primer.Stop();
                    timer_primer = new System.Windows.Threading.DispatcherTimer();
                    U = true_ovet - skip_otvet;
                    U = U / (min_count * 60 + sec_count);
                    U = U * 60;
                    GridGameFinish.Visibility = Visibility.Visible;
                    CountPrimerItog.Content = String.Format("Вы решили примеров: {0}", true_ovet);
                    CountTimeItog.Content = String.Format("За время: {0}:{1}", min, sec);
                    CountUItog.Content = String.Format("Ваша скорость: {0:#.##}", U);
                    CountGameToday.Content = String.Format("Попытка за сегодня: {0}", attempt.Attempt(@"Attempt.txt"));
                    stat.Statistic("ExStat.txt", String.Format("{0} \t {1}", U, DateTime.Now));
                    true_ovet = 0; false_otvet = 0;
                }
            }
        }

        private void fon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fon.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;

            ExitImage.Visibility = Visibility.Visible;
            ChetForm.Visibility = Visibility.Visible;
            PrimerLabel.Visibility = Visibility.Visible;
            OtvetTextBox.Visibility = Visibility.Visible;
            TimeLabel.Visibility = Visibility.Visible;
            CancelImage.Visibility = Visibility.Visible;
            SkipImage.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            label4.Visibility = Visibility.Visible;
            label4_Copy.Visibility = Visibility.Visible;
            skiplabel.Visibility = Visibility.Visible;
            falselabel.Visibility = Visibility.Visible;
            truelabel.Visibility = Visibility.Visible;
            HelpFaierImage.Visibility = Visibility.Visible;
            OtvetTextBox.Focus();

            exp = ge.GetExpression();
            PrimerLabel.Content = exp.ToString();

            timer_primer.Tick += new EventHandler(timer_primerTick);
            timer_primer.Interval = new TimeSpan(0, 0, 0, 1);
            timer_primer.Start();

        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitImage.Height = 77;
            ExitImage.Width = 90;
            ExitImage.Margin = new Thickness(5, 5, 0, 0);
            ExitImage.ToolTip = "Вернуться в меню";
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitImage.Height = 67;
            ExitImage.Width = 80;
            ExitImage.Margin = new Thickness(10, 10, 0, 0);
        }

        private void CancelImage_MouseLeave(object sender, MouseEventArgs e)
        {
            CancelImage.Height = 68;
            CancelImage.Width = 69;
        }

        private void CancelImage_MouseEnter(object sender, MouseEventArgs e)
        {
            CancelImage.Height = 73;
            CancelImage.Width = 74;
        }

        private void SkipImage_MouseEnter(object sender, MouseEventArgs e)
        {
            SkipImage.Height = 68;
            SkipImage.Width = 68;
            SkipImage.Margin = new Thickness(355, 410, -318, -374);
        }

        private void SkipImage_MouseLeave(object sender, MouseEventArgs e)
        {
            SkipImage.Height = 63;
            SkipImage.Width = 59;
            SkipImage.Margin = new Thickness(359, 411, -318, -374);
        }

        int true_ovet = 0, false_otvet = 0, skip_otvet = 0;

        private void MathForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (OtvetTextBox.Visibility == Visibility.Visible)
            {
                if (Key.Enter == e.Key)
                    CancelImage_MouseLeftButtonDown(sender, null);
            }
        }

        private void DedusImage_MouseLeave(object sender, MouseEventArgs e)
        {
            DedusImage.Height = 194;
            DedusImage.Width = 206;
            label.Visibility = Visibility.Visible;
            MagImage.Visibility = Visibility.Hidden;
        }

        private void DedusImage_MouseEnter(object sender, MouseEventArgs e)
        {
            DedusImage.Height = 209;
            DedusImage.Width = 221;
            label.Visibility = Visibility.Hidden;
            MagImage.Visibility = Visibility.Visible;
        }

        bool magic_true = true;
        int[] arr_answer;
        private void DedusImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridDedus.Visibility = Visibility.Visible;

            fon_Copy.Visibility = Visibility.Visible;
            label2_Copy.Visibility = Visibility.Visible;

            int[] arr = magic.MagicSquareChange();
            arr_answer = (int[])arr.Clone();
            int[] arr_to_textbox = new int[9];
            arr_to_textbox = magic.MagicSquareOnGrid(arr);

            if (arr_to_textbox[0] == 0)
            { textBox11.Text = ""; textBox11.IsEnabled = true; }
            else { textBox11.Text = arr_to_textbox[0].ToString(); textBox11.IsEnabled = false; }
            if (arr_to_textbox[1] == 0)
            { textBox12.Text = ""; textBox12.IsEnabled = true; }
            else { textBox12.Text = arr_to_textbox[1].ToString(); textBox12.IsEnabled = false; }
            if (arr_to_textbox[2] == 0)
            { textBox13.Text = ""; textBox13.IsEnabled = true; }
            else { textBox13.Text = arr_to_textbox[2].ToString(); textBox13.IsEnabled = false; }
            if (arr_to_textbox[3] == 0)
            { textBox21.Text = ""; textBox21.IsEnabled = true; }
            else { textBox21.Text = arr_to_textbox[3].ToString(); textBox21.IsEnabled = false; }
            if (arr_to_textbox[4] == 0)
            { textBox22.Text = ""; textBox22.IsEnabled = true; }
            else { textBox22.Text = arr_to_textbox[4].ToString(); textBox22.IsEnabled = false; }
            if (arr_to_textbox[5] == 0)
            { textBox23.Text = ""; textBox23.IsEnabled = true; }
            else { textBox23.Text = arr_to_textbox[5].ToString(); textBox23.IsEnabled = false; }
            if (arr_to_textbox[6] == 0)
            { textBox31.Text = ""; textBox31.IsEnabled = true; }
            else { textBox31.Text = arr_to_textbox[6].ToString(); textBox31.IsEnabled = false; }
            if (arr_to_textbox[7] == 0)
            { textBox32.Text = ""; textBox32.IsEnabled = true; }
            else { textBox32.Text = arr_to_textbox[7].ToString(); textBox32.IsEnabled = false; }
            if (arr_to_textbox[8] == 0)
            { textBox33.Text = ""; textBox33.IsEnabled = true; }
            else { textBox33.Text = arr_to_textbox[8].ToString(); textBox33.IsEnabled = false; }
        }

        private void fon_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridMagic.Visibility = Visibility.Visible;
            timer_magic.Tick += new EventHandler(timer_magicTick);
            timer_magic.Interval = new TimeSpan(0, 0, 0, 1);
            timer_magic.Start();
        }

        private void ExitImage2_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitImage2.Height = 77;
            ExitImage2.Width = 90;
            ExitImage2.Margin = new Thickness(5, 5, 0, 0);
        }

        private void ExitImage2_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitImage2.Height = 67;
            ExitImage2.Width = 80;
            ExitImage2.Margin = new Thickness(10, 10, 0, 0);
        }

        private void ExitImage2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridDedus.Visibility = Visibility.Hidden;
            GridMagic.Visibility = Visibility.Hidden;
            sec_count = 0;
            min_count = 0;
            timer_magic.Stop();
            timer_magic = new System.Windows.Threading.DispatcherTimer();
            //GridGameFinish.Visibility = Visibility.Visible;
            min_count = 0;
            sec_count = 0;
            TimeLabel2.Content = "00:00";
            magic_false_inLabel = 0;
            magic_true_inLabel = 0;
        }

        private void CancelMagic_MouseEnter(object sender, MouseEventArgs e)
        {
            CancelMagic.Height = 75;
            CancelMagic.Width = 91;
            CancelMagic.Margin = new Thickness(570, 204, 0, 0);
        }

        private void CancelMagic_MouseLeave(object sender, MouseEventArgs e)
        {
            CancelMagic.Height = 65;
            CancelMagic.Width = 81;
            CancelMagic.Margin = new Thickness(575, 209, 0, 0);
        }
        int magic_true_inLabel, magic_false_inLabel;
        private void CancelMagic_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (textBox11.Text == "" || Convert.ToInt32(textBox11.Text) != arr_answer[0]) magic_true = false;
            if (textBox12.Text == "" || Convert.ToInt32(textBox12.Text) != arr_answer[1]) magic_true = false;
            if (textBox13.Text == "" || Convert.ToInt32(textBox13.Text) != arr_answer[2]) magic_true = false;
            if (textBox21.Text == "" || Convert.ToInt32(textBox21.Text) != arr_answer[3]) magic_true = false;
            if (textBox22.Text == "" || Convert.ToInt32(textBox22.Text) != arr_answer[4]) magic_true = false;
            if (textBox23.Text == "" || Convert.ToInt32(textBox23.Text) != arr_answer[5]) magic_true = false;
            if (textBox31.Text == "" || Convert.ToInt32(textBox31.Text) != arr_answer[6]) magic_true = false;
            if (textBox32.Text == "" || Convert.ToInt32(textBox32.Text) != arr_answer[7]) magic_true = false;
            if (textBox33.Text == "" || Convert.ToInt32(textBox33.Text) != arr_answer[8]) magic_true = false;
            //}
            //else magic_true = false;
            if (magic_true)
            {
                magic_true_inLabel++;
                truelabel_Copy.Content = magic_true_inLabel;
                MusicTrue.SoundLocation = @"Верный.wav";
                MusicTrue.Play();
            }
            else
            {
                magic_false_inLabel++;
                falselabel_Copy.Content = magic_false_inLabel;
                MusicFalse.SoundLocation = @"Неверный.wav";
                MusicFalse.Play();
            }
            arr_answer = magic.MagicSquareChange();
            int[] arr_to_textbox = new int[9];
            arr_to_textbox = magic.MagicSquareOnGrid(arr_answer);

            if (arr_to_textbox[0] == 0)
            { textBox11.Text = ""; textBox11.IsEnabled = true; }
            else { textBox11.Text = arr_to_textbox[0].ToString(); textBox11.IsEnabled = false; }
            if (arr_to_textbox[1] == 0)
            { textBox12.Text = ""; textBox12.IsEnabled = true; }
            else { textBox12.Text = arr_to_textbox[1].ToString(); textBox12.IsEnabled = false; }
            if (arr_to_textbox[2] == 0)
            { textBox13.Text = ""; textBox13.IsEnabled = true; }
            else { textBox13.Text = arr_to_textbox[2].ToString(); textBox13.IsEnabled = false; }
            if (arr_to_textbox[3] == 0)
            { textBox21.Text = ""; textBox21.IsEnabled = true; }
            else { textBox21.Text = arr_to_textbox[3].ToString(); textBox21.IsEnabled = false; }
            if (arr_to_textbox[4] == 0)
            { textBox22.Text = ""; textBox22.IsEnabled = true; }
            else { textBox22.Text = arr_to_textbox[4].ToString(); textBox22.IsEnabled = false; }
            if (arr_to_textbox[5] == 0)
            { textBox23.Text = ""; textBox23.IsEnabled = true; }
            else { textBox23.Text = arr_to_textbox[5].ToString(); textBox23.IsEnabled = false; }
            if (arr_to_textbox[6] == 0)
            { textBox31.Text = ""; textBox31.IsEnabled = true; }
            else { textBox31.Text = arr_to_textbox[6].ToString(); textBox31.IsEnabled = false; }
            if (arr_to_textbox[7] == 0)
            { textBox32.Text = ""; textBox32.IsEnabled = true; }
            else { textBox32.Text = arr_to_textbox[7].ToString(); textBox32.IsEnabled = false; }
            if (arr_to_textbox[8] == 0)
            { textBox33.Text = ""; textBox33.IsEnabled = true; }
            else { textBox33.Text = arr_to_textbox[8].ToString(); textBox33.IsEnabled = false; }
            magic_true = true;
        }

        private void ExitImage_Pass_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitImage_Pass.Height = 95;
            ExitImage_Pass.Width = 113;
            ExitImage_Pass.Margin = new Thickness(5, 417, 0, 0);
            label1.Visibility = Visibility.Hidden;

        }

        private void ExitImage_Pass_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitImage_Pass.Height = 85;
            ExitImage_Pass.Width = 103;
            ExitImage_Pass.Margin = new Thickness(10, 422, 0, 0);
        }

        private void ExitImage_Pass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridPassword.Visibility = Visibility.Hidden;
        }

        private void CancelImagePass_MouseLeave(object sender, MouseEventArgs e)
        {
            CancelImagePass.Height = 79;
            CancelImagePass.Width = 100;
            CancelImagePass.Margin = new Thickness(296, 279, 0, 0);
        }

        private void CancelImagePass_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (password == passwordBox.Password)
            {
                GridSetting.Visibility = Visibility.Visible;
                passwordBox.Password = "";
            }
            else
            {
                MessageBox.Show("Пароль введен неверно!");
                passwordBox.Password = "";
            }
        }

        private void BTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void CancelImagePass_MouseEnter(object sender, MouseEventArgs e)
        {
            CancelImagePass.Height = 89;
            CancelImagePass.Width = 110;
            CancelImagePass.Margin = new Thickness(291, 274, 0, 0);
        }

        private void CountTrueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void CountMinutTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void OtvetTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void checkBoxTimeSetting_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxCountTruePrimer.IsChecked = false;
            CountTrueTextBox.IsEnabled = false;
            CountMinutTextBox.IsEnabled = true;
        }

        private void checkBoxTimeSetting_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBoxCountTruePrimer.IsChecked = true;
            CountTrueTextBox.IsEnabled = true;
            CountMinutTextBox.IsEnabled = false;
        }

        private void checkBoxCountTruePrimer_Checked(object sender, RoutedEventArgs e)
        {
            checkBoxTimeSetting.IsChecked = false;
        }

        private void checkBoxCountTruePrimer_Unchecked(object sender, RoutedEventArgs e)
        {
            checkBoxTimeSetting.IsChecked = true;
        }

        private void ExitImage_Sett_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitImage_Sett.Height = 95;
            ExitImage_Sett.Width = 113;
            ExitImage_Sett.Margin = new Thickness(5, 417, 0, 0);
        }

        private void ExitImage_Sett_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitImage_Sett.Height = 85;
            ExitImage_Sett.Width = 103;
            ExitImage_Sett.Margin = new Thickness(10, 422, 0, 0);
        }

        private void ExitImage_Sett_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (BTextBox.Text != "" && CountTrueTextBox.Text != "" && CountMinutTextBox.Text != "")
            {
                if (BTextBox.Foreground != Brushes.Red || CountTrueTextBox.Foreground != Brushes.Red || CountMinutTextBox.Foreground != Brushes.Red)
                {
                    ge = new GeneratorExpression(10, Convert.ToInt32(BTextBox.Text));
                    GridSetting.Visibility = Visibility.Hidden;
                    GridPassword.Visibility = Visibility.Hidden;
                    time_user = Convert.ToInt32(CountMinutTextBox.Text);
                    user_primer = Convert.ToInt32(CountTrueTextBox.Text);
                    time_user_magic = Convert.ToInt32(BTextBox_Copy.Text);
                }
                else
                {
                    MessageBox.Show("Данные введены неверно!");
                }
            }
            else
            {
                MessageBox.Show("Данные введены некорректно!");
            }
        }

        private void GridPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CancelImagePass_MouseLeftButtonDown(this, null);
        }

        private void BTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pos = BTextBox.SelectionStart;
            int len1 = BTextBox.Text.Length;
            BTextBox.Text = BTextBox.Text.Replace(" ", "");
            if (BTextBox.Text != "")
            {
                BTextBox.Text = Convert.ToInt32(BTextBox.Text).ToString();
                int len2 = BTextBox.Text.Length;
                if (BTextBox.Text != "0") BTextBox.SelectionStart = pos - (len1 - len2);
                else BTextBox.SelectionStart = 0;

                int temp = Convert.ToInt32(BTextBox.Text);
                if (temp < 20) BTextBox.Foreground = Brushes.Red;
                else BTextBox.Foreground = Brushes.Black;
            }
        }

        private void HelpFaierImage_MouseEnter(object sender, MouseEventArgs e)
        {
            HelpFaierImage.Height = 66;
            HelpFaierImage.Width = 68;
            HelpFaierImage.Margin = new Thickness(601, 440, -564, -401);
        }

        private void HelpFaierImage_MouseLeave(object sender, MouseEventArgs e)
        {
            HelpFaierImage.Height = 56;
            HelpFaierImage.Width = 58;
            HelpFaierImage.Margin = new Thickness(606, 445, -564, -401);
        }

        private void CountTrueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pos = CountTrueTextBox.SelectionStart;
            int len1 = CountTrueTextBox.Text.Length;
            CountTrueTextBox.Text = CountTrueTextBox.Text.Replace(" ", "");
            if (CountTrueTextBox.Text != "")
            {
                CountTrueTextBox.Text = Convert.ToInt32(CountTrueTextBox.Text).ToString();
                int len2 = CountTrueTextBox.Text.Length;
                if (CountTrueTextBox.Text != "0") CountTrueTextBox.SelectionStart = pos - (len1 - len2);
                else CountTrueTextBox.SelectionStart = 0;
                int temp;
                temp = Convert.ToInt32(CountTrueTextBox.Text);
                if (temp < 5) CountTrueTextBox.Foreground = Brushes.Red;
                else CountTrueTextBox.Foreground = Brushes.Black;
            }
        }

        private void CountMinutTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pos = CountMinutTextBox.SelectionStart;
            int len1 = CountMinutTextBox.Text.Length;
            CountMinutTextBox.Text = CountMinutTextBox.Text.Replace(" ", "");
            if (CountMinutTextBox.Text != "")
            {
                CountMinutTextBox.Text = Convert.ToInt32(CountMinutTextBox.Text).ToString();
                int len2 = CountMinutTextBox.Text.Length;
                if (CountMinutTextBox.Text != "0") CountMinutTextBox.SelectionStart = pos - (len1 - len2);
                else CountMinutTextBox.SelectionStart = 0;

                int temp = Convert.ToInt32(CountMinutTextBox.Text);
                if (temp < 1) CountMinutTextBox.Foreground = Brushes.Red;
                else CountMinutTextBox.Foreground = Brushes.Black;
            }
        }

        private void ExitImageFinish_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitImageFinish.Height = 77;
            ExitImageFinish.Width = 90;
            ExitImageFinish.Margin = new Thickness(5, 436, 0, 0);
        }

        private void ExitImageFinish_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitImageFinish.Height = 67;
            ExitImageFinish.Width = 80;
            ExitImageFinish.Margin = new Thickness(10, 441, 0, 0);
        }

        private void OtvetTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Key.Right == e.Key || Key.Left == e.Key || Key.LeftShift == e.Key || Key.RightShift == e.Key)
            {
                SkipImage_MouseLeftButtonDown(sender, null);
            }
        }

        private void OtvetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pos = OtvetTextBox.SelectionStart;
            int len1 = OtvetTextBox.Text.Length;
            OtvetTextBox.Text = OtvetTextBox.Text.Replace(" ", "");
            if (OtvetTextBox.Text != "")
            {
                OtvetTextBox.Text = Convert.ToInt32(OtvetTextBox.Text).ToString();
                int len2 = OtvetTextBox.Text.Length;
                if (OtvetTextBox.Text != "0") OtvetTextBox.SelectionStart = pos - (len1 - len2);
                else OtvetTextBox.SelectionStart = 0;
            }
        }

        private void HelpTimeCheck_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Укажите время длительности игры в минутах.");
        }

        private void HelpCountPrimer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Как только игрок решит правильно указанное количество примеров, игра закончится.");
        }

        private void HelpLimit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Примеры в игре составляет компьютер. Укажите максимальное значение, которое может быть выведено в примере.");
        }

        private void IgrekImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridPassword.Visibility = Visibility.Visible;
            passwordBox.Focus();
        }

        private void IgrekImage_MouseEnter(object sender, MouseEventArgs e)
        {
            IgrekImage.Height = 189;
            IgrekImage.Width = 230;
            IgrekImage.Margin = new Thickness(121, 323, 340, 0);
            label.Visibility = Visibility.Hidden;
            Settingimage.Visibility = Visibility.Visible;
        }

        private void IgrekImage_MouseLeave(object sender, MouseEventArgs e)
        {
            IgrekImage.Height = 174;
            IgrekImage.Width = 217;
            IgrekImage.Margin = new Thickness(131, 333, 340, 0);
            label.Visibility = Visibility.Visible;
            Settingimage.Visibility = Visibility.Hidden;
        }

        private void SimkaImage_MouseEnter(object sender, MouseEventArgs e)
        {
            SimkaImage.Height = 245;
            SimkaImage.Width = 166;
            SimkaImage.Margin = new Thickness(526, 113, 0, 0);
            StatImage.Visibility = Visibility.Visible;
            label.Visibility = Visibility.Hidden;
        }

        private void SimkaImage_MouseLeave(object sender, MouseEventArgs e)
        {
            SimkaImage.Height = 230;
            SimkaImage.Width = 152;
            SimkaImage.Margin = new Thickness(536, 123, 0, 0);
            StatImage.Visibility = Visibility.Hidden;
            label.Visibility = Visibility.Visible;
        }
List<string> strings = new List<string>();
        private void SimkaImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridRecord.Visibility = Visibility.Visible;
            listBox.Items.Clear();

            if (ExpressionRadioButton.IsChecked.Value)                
                strings = stat.Conclusion("ExStat.txt");
            else strings = stat.Conclusion("MagStat.txt");
            for (int i= 0; i < strings.Count(); i++)
            {
                listBox.Items.Add(strings[i]);
            }            
        }

        private void HelpTimeCheck_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Укажите, какая должна быть длительность игры в минутах.");
        }

        private void BTextBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            int pos = BTextBox_Copy.SelectionStart;
            int len1 = BTextBox_Copy.Text.Length;
            BTextBox_Copy.Text = BTextBox_Copy.Text.Replace(" ", "");
            if (BTextBox_Copy.Text != "")
            {
                BTextBox_Copy.Text = Convert.ToInt32(BTextBox_Copy.Text).ToString();
                int len2 = BTextBox_Copy.Text.Length;
                if (BTextBox_Copy.Text != "0") BTextBox_Copy.SelectionStart = pos - (len1 - len2);
                else BTextBox_Copy.SelectionStart = 0;

                int temp = Convert.ToInt32(BTextBox_Copy.Text);
                if (temp < 1) BTextBox_Copy.Foreground = Brushes.Red;
                else BTextBox_Copy.Foreground = Brushes.Black;
            }
        }

        private void ExitImage2_Copy_MouseEnter(object sender, MouseEventArgs e)
        {
            ExitImage2_Copy.Height = 77;
            ExitImage2_Copy.Width = 90;
            ExitImage2_Copy.Margin = new Thickness(25, 381, 0, 0);
        }

        private void ExitImage2_Copy_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitImage2_Copy.Height = 67;
            ExitImage2_Copy.Width = 80;
            ExitImage2_Copy.Margin = new Thickness(30, 386, 0, 0);
        }

        private void ExitImage2_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridRecord.Visibility = Visibility.Hidden;
        }

        private void MagicSquareRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();

            strings = stat.Conclusion("MagStat.txt");
            for (int i = 0; i < strings.Count(); i++)
            {
                listBox.Items.Add(strings[i]);
            }
        }

        private void ExpressionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            listBox.Items.Clear();
            strings = stat.Conclusion("ExStat.txt");
                for (int i = 0; i < strings.Count(); i++)
                {
                    listBox.Items.Add(strings[i]);
                }
        }

        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fon_MouseLeftButtonDown(sender, null);
        }

        private void ExitImageFinish_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (GridFaier.Visibility == Visibility.Visible)
            {
                GridFaier.Visibility = Visibility.Hidden;

                ExitImage.Visibility = Visibility.Hidden;
                ChetForm.Visibility = Visibility.Hidden;
                PrimerLabel.Visibility = Visibility.Hidden;
                OtvetTextBox.Visibility = Visibility.Hidden;
                TimeLabel.Visibility = Visibility.Hidden;
                CancelImage.Visibility = Visibility.Hidden;
                SkipImage.Visibility = Visibility.Hidden;
                label3.Visibility = Visibility.Hidden;
                label4.Visibility = Visibility.Hidden;
                label4_Copy.Visibility = Visibility.Hidden;
                skiplabel.Visibility = Visibility.Hidden;
                falselabel.Visibility = Visibility.Hidden;
                truelabel.Visibility = Visibility.Hidden;
                HelpFaierImage.Visibility = Visibility.Hidden;
            }
            GridGameFinish.Visibility = Visibility.Hidden;
            if (GridMagic.Visibility == Visibility.Visible)
            {
                GridMagic.Visibility = Visibility.Hidden;
                GridDedus.Visibility = Visibility.Hidden;
                timer_magic.Stop();
                timer_magic = new System.Windows.Threading.DispatcherTimer(); GridGameFinish.Visibility = Visibility.Visible;
                min_count = 0;
                sec_count = 0;
                TimeLabel2.Content = "00:00";
            }
        }

        private void HelpFaierImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(checkBoxCountTruePrimer.IsChecked.Value)
                MessageBox.Show(String.Format("Игра закончится, когда Вы ответите правильно на {0} вопросов", CountTrueTextBox.Text));
            if (checkBoxTimeSetting.IsChecked.Value)
                MessageBox.Show(String.Format("Игра закончится, когда время будет равно {0} минутам", CountMinutTextBox.Text));
        }

        private void SkipImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            skip_otvet++;
            skiplabel.Content = skip_otvet;

            exp = ge.GetExpression();
            PrimerLabel.Content = exp.ToString();
            OtvetTextBox.Text = "";
        }

        private void CancelImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            if (OtvetTextBox.Text == exp.c.ToString())
            {
                true_ovet++;
                truelabel.Content = true_ovet;
                MusicTrue.SoundLocation = @"Верный.wav";
                MusicTrue.Play();
            }
            else
            {
                false_otvet++;
                falselabel.Content = false_otvet;
                MusicFalse.SoundLocation = @"Неверный.wav";
                MusicFalse.Play();
            }

            if (checkBoxCountTruePrimer.IsChecked.Value)
            {
                if (user_primer == true_ovet)
                {
                    timer_primer.Stop();
                    U = true_ovet - skip_otvet;
                    U = U / (min_count * 60 + sec_count);
                    U = U * 60;
                    timer_primer = new System.Windows.Threading.DispatcherTimer(); GridGameFinish.Visibility = Visibility.Visible;
                    CountPrimerItog.Content = String.Format("Вы решили примеров: {0}", true_ovet);
                    CountTimeItog.Content = String.Format("За время: {0}", TimeLabel.Content);
                    CountUItog.Content = String.Format("Ваша скорость: {0:#.##} пр/мин", U);
                    CountGameToday.Content = String.Format("Попытка за сегодня: {0}", attempt.Attempt(@"Attempt.txt"));
                    stat.Statistic("ExStat.txt", String.Format("{0:#.##}\t{1}", U, DateTime.Now));
                    true_ovet = 0; false_otvet = 0;
                }
            }

            exp = ge.GetExpression();
            PrimerLabel.Content = exp.ToString();
            OtvetTextBox.Text = "";
        }

        private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridFaier.Visibility = Visibility.Hidden;

            ExitImage.Visibility = Visibility.Hidden;
            ChetForm.Visibility = Visibility.Hidden;
            PrimerLabel.Visibility = Visibility.Hidden;
            OtvetTextBox.Visibility = Visibility.Hidden;
            TimeLabel.Visibility = Visibility.Hidden;
            CancelImage.Visibility = Visibility.Hidden;
            SkipImage.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            label4.Visibility = Visibility.Hidden;
            label4_Copy.Visibility = Visibility.Hidden;
            skiplabel.Visibility = Visibility.Hidden;
            falselabel.Visibility = Visibility.Hidden;
            truelabel.Visibility = Visibility.Hidden;
            HelpFaierImage.Visibility = Visibility.Hidden;
            sec_count = 0;
            min_count = 0;
            falselabel.Content = "0";
            skiplabel.Content = "0";
            truelabel.Content = "0";
            timer_primer.Stop();
            timer_primer = new System.Windows.Threading.DispatcherTimer();
            count_game = attempt.Attempt(@"Attempt.txt");
        }
    }
}
