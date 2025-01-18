using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace PasLogMeng
{
    public class TextToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value as string) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class MainWindow : Window
    {
        // Коллекция паролей
        public ObservableCollection<PasswordEntry> Passwords { get; set; }
        // Список категорий
        public ObservableCollection<string> Categories { get; set; }
       // свойство пароля
        public string Password { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация данных
            Passwords = new ObservableCollection<PasswordEntry>();
            Categories = new ObservableCollection<string>
            {
                "Discord", "Google", "Epic", "Steam"
            };

            // Привязка данных
            DataContext = this;
        }

        // Добавление пароля
        private void AddPassword_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryComboBox.Text) ||
                string.IsNullOrWhiteSpace(NameInput.Text) ||
                string.IsNullOrWhiteSpace(EmailInput.Text) ||
                string.IsNullOrWhiteSpace(LoginInput.Text) ||
                string.IsNullOrWhiteSpace(PasswordInput.Password))
            {
                MessageBox.Show("Заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Passwords.Add(new PasswordEntry
            {
                Category = CategoryComboBox.Text,
                Name = NameInput.Text,
                Email = EmailInput.Text,
                Login = LoginInput.Text,
                Password = PasswordInput.Password,
                IsFavorite = false
            });

            ClearInputs();
        }
        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                Password = passwordBox.Password; // Обновление привязки вручную
            }
        }
        // Очистка полей после добавления
        private void ClearInputs()
        {
            CategoryComboBox.Text = string.Empty;
            NameInput.Clear();
            EmailInput.Clear();
            LoginInput.Clear();
            PasswordInput.Clear();
        }

        // Копирование в буфер обмена
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is PasswordEntry entry)
            {
                Clipboard.SetText($"Email: {entry.Email}\nLogin: {entry.Login}\nPassword: {entry.Password}");
                MessageBox.Show("Скопировано в буфер обмена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Добавление новой категории
        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewCategoryInput.Text) && !Categories.Contains(NewCategoryInput.Text))
            {
                Categories.Add(NewCategoryInput.Text);
                NewCategoryInput.Clear();
            }
            else
            {
                MessageBox.Show("Категория не может быть пустой или уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void PasswordGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    // Класс паролей
    public class PasswordEntry
    {
        public string Category { get; set; } // Категория
        public string Name { get; set; }    // Название
        public string Email { get; set; }   // Почта
        public string Login { get; set; }   // Логин
        public string Password { get; set; } // Пароль
        public bool IsFavorite { get; set; } // Избранное
    }

    // Конвертер для видимости плейсхолдера

}