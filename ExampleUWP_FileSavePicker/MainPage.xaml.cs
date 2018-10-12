using ExampleUWP_FileSavePicker.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ExampleUWP_FileSavePicker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Member currentMember;
        private Member member;

        public MainPage()

        {
            this.currentMember = new Member();
            this.InitializeComponent();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            currentMember.Name = this.txt_name.Text;
            currentMember.Email = this.txt_email.Text;
            currentMember.Phone = this.txt_phone.Text;
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "Test.txt";
            StorageFile file = await savePicker.PickSaveFileAsync();
            string jsonMember = JsonConvert.SerializeObject(this.currentMember);
            await FileIO.WriteTextAsync(file, jsonMember);
        }

        private async void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            StorageFile file = await openPicker.PickSingleFileAsync();
            string content = await Windows.Storage.FileIO.ReadTextAsync(file);
            member = JsonConvert.DeserializeObject<Member>(content);
            this.txt_name.Text = member.Name;
            this.txt_email.Text = member.Email;
            this.txt_phone.Text = member.Phone;
        }
    }
}
