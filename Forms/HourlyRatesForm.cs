using AlJamal.Database;

namespace AlJamal.Forms;

public partial class HourlyRatesForm : Form
{
    private const int SnookerTypeId = 1;
    private const int BlackTypeId = 2;
    private const int Snooker7TypeId = 3;

    public HourlyRatesForm()
    {
        InitializeComponent();
    }

    private void HourlyRatesForm_Load(object? sender, EventArgs e)
    {
        var types = BilliardRepository.GetTableTypes();
        var snooker = types.FirstOrDefault(t => t.TableTypeId == SnookerTypeId);
        var snooker7 = types.FirstOrDefault(t => t.TableTypeId == Snooker7TypeId);
        var black = types.FirstOrDefault(t => t.TableTypeId == BlackTypeId);

        if (snooker != null)
        {
            numSnookerFirst.Value = snooker.FirstHourRate;
            numSnookerAdditional.Value = snooker.AdditionalHourRate;
        }

        if (snooker7 != null)
        {
            numSnooker7First.Value = snooker7.FirstHourRate;
            numSnooker7Additional.Value = snooker7.AdditionalHourRate;
        }

        if (black != null)
        {
            numBlackFirst.Value = black.FirstHourRate;
            numBlackAdditional.Value = black.AdditionalHourRate;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        try
        {
            BilliardRepository.UpdatePricingRates(SnookerTypeId, numSnookerFirst.Value, numSnookerAdditional.Value);
            BilliardRepository.UpdatePricingRates(Snooker7TypeId, numSnooker7First.Value, numSnooker7Additional.Value);
            BilliardRepository.UpdatePricingRates(BlackTypeId, numBlackFirst.Value, numBlackAdditional.Value);
            MessageBox.Show("تم حفظ الأسعار.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e) => Close();
}
