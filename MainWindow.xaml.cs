using KnapsackCipher.Logic;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace KnapsackCipher
{
    public sealed partial class MainWindow : Window
    {
        private KnapsackKeyGenerator _keyGenerator;

        public MainWindow()
        {
            this.InitializeComponent();
            _keyGenerator = new KnapsackKeyGenerator();
        }

        private void GenerateKeysButton_Click(object sender, RoutedEventArgs e)
        {
            _keyGenerator.GenerateKeys(8);
            PublicKeyTextBox.Text = string.Join(", ", _keyGenerator.PublicKey);
            PrivateKeyTextBox.Text = string.Join(", ", _keyGenerator.SuperIncreasingSequence);
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var inputMessage = InputMessageTextBox.Text;
                var binaryMessage = inputMessage.Select(c => int.Parse(c.ToString())).ToList();

                if(binaryMessage.Count != _keyGenerator.PublicKey.Count)
                {
                    throw new Exception("Message length must match key length.");
                }

                var encryptedMessage = KnapsackEncryptor.EncryptBlock(_keyGenerator.PublicKey, binaryMessage);
                EncryptedMessageTextBox.Text = encryptedMessage.ToString();
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var encryptedMessage = int.Parse(EncryptedMessageTextBox.Text);
                var decryptedMessage = KnapsackDecryptor.DecryptBlock(
                    encryptedMessage,
                    _keyGenerator.SuperIncreasingSequence,
                    _keyGenerator.MultiplierInverse,
                    _keyGenerator.Modulus);

                DecryptedMessageTextBox.Text = string.Join("", decryptedMessage);
            }
            catch(Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ShowError(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Error",
                Content = message,
                CloseButtonText = "OK"
            };
            _ = dialog.ShowAsync();
        }
    }
}
