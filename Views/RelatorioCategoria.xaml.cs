using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace MauiAppMinhasCompras.Views;

public partial class RelatorioCategoria : ContentPage
{
    ObservableCollection<dynamic> relatorio = new ObservableCollection<dynamic>();

    public RelatorioCategoria()
    {
        InitializeComponent();
        lst_relatorio.ItemsSource = relatorio;
    }

    protected async override void OnAppearing()
    {
        try
        {
            relatorio.Clear();

            List<Produto> produtos = await App.Db.GetAll();

            var dados = produtos
                .GroupBy(p => string.IsNullOrWhiteSpace(p.Categoria) ? "Sem Categoria" : p.Categoria)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(p => p.Total) });

            foreach (var item in dados)
            {
                relatorio.Add(item);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}