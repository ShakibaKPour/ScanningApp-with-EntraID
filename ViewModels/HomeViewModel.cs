﻿using RepRepair.Extensions;
using RepRepair.Services.Language;
using RepRepair.Services.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RepRepair.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public ObservableCollection<string> AvailableLanguages { get; } = new ObservableCollection<string>
    {
        "en-US", "es-ES", "it-IT", "sv-SE", "fr-FR", "fa-IR", "de-DE", "da-DK"
    };
    public ICommand ScanCommand { get; set; }

    private readonly LanguageSettingsService _languageSettingsService;
    public string SelectedLanguage
    {
        get => _languageSettingsService.CurrentLanguage;
        set
        {
            if (_languageSettingsService.CurrentLanguage != value)
            {
                _languageSettingsService.CurrentLanguage = value;
                OnPropertyChanged(nameof(SelectedLanguage));
            }
        }
    }

    public HomeViewModel()
    {
        _languageSettingsService = ServiceHelper.GetService<LanguageSettingsService>();
        ScanCommand = new Command(async () => await OnScanAsync());

    }

    private async Task OnScanAsync()
    {
        await Shell.Current.GoToAsync("//ScanPage");
    }
}
