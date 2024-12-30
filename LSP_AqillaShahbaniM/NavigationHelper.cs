using System.Windows.Forms;

namespace LSP_AqillaShahbaniM
{
    public abstract class BaseFormNavigation
    {
        public abstract void NavigateToForm(Form currentForm, Form targetForm);
    }
}

namespace LSP_AqillaShahbaniM
{
    public class NavigationHelper : BaseFormNavigation
    {
        public override void NavigateToForm(Form currentForm, Form targetForm)
        {
            targetForm.Show();
            currentForm.Hide();
        }

        public void NavigateToFormClose(Form currentForm, Form targetForm)
        {
            targetForm.Show();
            currentForm.Close();
        }

        public void NavigateToTambahEditMember(Form currentForm, string parameter)
        {
            var formTambahEditMember = new tambahEditMember(parameter);
            formTambahEditMember.Show();
            currentForm.Close();
        }

        public void NavigateToTambahEditBuku(Form currentForm, string parameter)
        {
            var formTambahEditBuku = new tambahEditBuku(parameter);
            formTambahEditBuku.Show();
            currentForm.Close();
        }

        public void NavigateToDetailPengembalian(Form currentForm, string parameter)
        {
            var formDetailPengembalian = new detailPengembalianBuku(parameter);
            formDetailPengembalian.Show();
            currentForm.Close();
        }
    }
}
