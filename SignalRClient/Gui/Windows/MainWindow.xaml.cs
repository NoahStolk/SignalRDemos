using SignalRClient.Code;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SignalRClient.Gui.Windows
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Dispatcher.UnhandledException += (sender, e) =>
			{
				MessagesList.Items.Add(new TextBlock { Text = e.Exception.Message, Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)) });
				e.Handled = true;
			};
		}

		private async void ToggleConnectButton_Click(object sender, RoutedEventArgs e)
		{
			await SimpleChatClient.Instance.ToggleConnection(
				receiveJoin: (param) => Dispatcher.Invoke(() =>
				{
					MessagesList.Items.Add(new TextBlock { Text = $"{DateTime.Now} - {param.User.UserName} joined.", Background = new SolidColorBrush(Color.FromArgb(127, 63, 191, 63)) });

					ScrollToEnd();
				}),
				receiveLeave: (param) => Dispatcher.Invoke(() =>
				{
					MessagesList.Items.Add(new TextBlock { Text = $"{DateTime.Now} - {param.User.UserName} left.", Background = new SolidColorBrush(Color.FromArgb(127, 191, 63, 63)) });

					ScrollToEnd();
				}),
				receiveMessage: (param) => Dispatcher.Invoke(() =>
				{
					MessagesList.Items.Add(new TextBlock { Text = $"{DateTime.Now} - {param.User.UserName}: {param.Message}", Background = new SolidColorBrush(Color.FromArgb(127, 63, 191, 63)) });

					ScrollToEnd();
				}),
				receiveColors: (param) => Dispatcher.Invoke(() =>
				{
					MessageBox.Show("TODO: Change colors.");
				}));

			ConnectButton.Content = SimpleChatClient.Instance.IsConnected ? "Disconnect" : "Connect";
		}

		private void ScrollToEnd()
		{
			if (VisualTreeHelper.GetChildrenCount(MessagesList) > 0)
			{
				Border border = (Border)VisualTreeHelper.GetChild(MessagesList, 0);
				ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
				scrollViewer.ScrollToBottom();
			}
		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			MessagesList.Items.Clear();
		}
	}
}