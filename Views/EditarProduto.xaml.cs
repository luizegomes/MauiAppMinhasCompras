using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    public List<string> Categorias { get; set; } // Lista de Categorias
    public EditarProduto()
	{
		InitializeComponent();

        // Exemplo de categorias para teste
        Categorias = new List<string>
        {
            "Sem Categoria",
            "Alimentos",
            "Bebidas",
            "Limpeza"
        };

        BindingContext = this; // Define o BindingContext
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Produto produto_anexado = BindingContext as Produto;
            if (produto_anexado == null) return;

            produto_anexado.Descricao = txt_descricao.Text;
            produto_anexado.Quantidade = Convert.ToDouble(txt_quantidade.Text);
            produto_anexado.Preco = Convert.ToDouble(txt_preco.Text);

            // ? Captura a nova categoria do Picker
            produto_anexado.Categoria = picker_categoria.SelectedItem?.ToString() ?? "Sem Categoria";

            await App.Db.Update(produto_anexado);

            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            await Navigation.PopAsync();

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void PickerCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        if (picker != null)
        {
            Produto produto_anexado = BindingContext as Produto;
            if (produto_anexado != null)
            {
                produto_anexado.Categoria = picker.SelectedItem as string ?? "Sem Categoria";
            }
        }
    }
}