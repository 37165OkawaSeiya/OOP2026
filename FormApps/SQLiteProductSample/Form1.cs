using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Linq;

namespace SQLiteProductSample;

public partial class Form1 : Form {
    // DataGridViewへ表示する商品の一覧
    private readonly BindingList<Product> _products = new();
    // DB操作を担当するRepository
    private readonly ProductRepository _repository = new();

    public Form1() {
        InitializeComponent();
        // ProductクラスのプロパティからDate
        dgvProducts.AutoGenerateColumns = true;
        dgvProducts.DataSource = _products;

        ReloadProducts();

        tsslMessage.Text = $"DB: {Database.FilePath}";
    }

    private void btAdd_Click(object sender, EventArgs e) {
        if (!TryGetInput(out string name, out int price))
            return;
        try {
            _repository.Add(name, price);

            ReloadProducts();
            ClearInput();

            tsslMessage.Text = "商品を登録しました。";
        }
        catch (Exception ex) {
            ShowError("登録エラー", ex);
        }
    }

    private void btUpdate_Click(object sender, EventArgs e) {
        if (dgvProducts.CurrentRow?.DataBoundItem is not Product selectedProduct) {
            tsslMessage.Text = "修正する商品を選択してください。";
            return;
        }
        if (!TryGetInput(out string name, out int price))
            return;
        try {
            selectedProduct.Name = name;
            selectedProduct.Price = price;

            _repository.Update(selectedProduct);

            ReloadProducts();
            ClearInput();

            tsslMessage.Text = "商品を修正しました。";
        }
        catch (Exception ex) {
            ShowError("修正エラー",ex);
        }
    }

    private void btDelete_Click(object sender, EventArgs e) {
        if (dgvProducts.CurrentRow?.DataBoundItem is not Product selectedProduct) {
            tsslMessage.Text = "削除する商品を選択してください。";
            return;
        }
        //誤削除防止の確認
        if (MessageBox.Show(
            $"「{selectedProduct.Name}」を削除しますか？",
            "削除確認",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question) != DialogResult.Yes) {
            return;
        }

        try {
            //Idを使ってDBから1件削除する。
            _repository.Delete(selectedProduct.Id);

            ReloadProducts();
            ClearInput();
            tsslMessage.Text = "商品を削除しました。";
        }
        catch (Exception ex) {
            ShowError("削除エラー", ex);
        }
    }

    private void btClear_Click(object sender, EventArgs e) {
        ClearInput();
        dgvProducts.ClearSelection();
        tsslMessage.Text = "入力欄をクリアしました。";
    }

    private void dgvProducts_SelectionChanged(object sender, EventArgs e) {
        if (dgvProducts.CurrentRow?.DataBoundItem is not Product product)
            return;

        // 選択した商品のデータを入力へ表示する
        tbName.Text = product.Name;
        tbPrice.Text = product.Price.ToString();
    }

    private void ReloadProducts() {
        _products.Clear();
        foreach (var product in _repository.GetAll()) {
            _products.Add(product);
        }
        dgvProducts.ClearSelection();
    }

    private bool TryGetInput(out string name, out int price) {
        name = tbName.Text.Trim();

        if (string.IsNullOrWhiteSpace(name)) {
            price = 0;
            tsslMessage.Text = "商品名を入力してください。";
            tbName.Focus();
            return false;
        }

        if (!int.TryParse(tbPrice.Text, out price) || price < 0) {
            tsslMessage.Text = "価格は0以上の整数で入力してください。";
            tbPrice.Focus();
            tbPrice.SelectAll();
            return false;
        }
        return true;
    }

    private void ClearInput() {
        tbName.Clear();
        tbPrice.Clear();
        tbName.Focus();
    }

    private void ShowError(string title, Exception ex) {
        tsslMessage.Text = title;
        MessageBox.Show(
            ex.Message,
            title,
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }

    private void dgvProducts_SelectionChange(object sender, EventArgs e) {
        if (dgvProducts.CurrentRow?.DataBoundItem is not Product product)
            return;
        tbName.Text = product.Name;
        tbPrice.Text = product.Price.ToString();
    }
}
