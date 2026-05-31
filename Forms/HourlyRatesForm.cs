using AlJamal.Database;

namespace AlJamal.Forms;

public partial class HourlyRatesForm : Form
{
    private int _snookerTypeId = 1;
    private int _blackTypeId = 2;

    public HourlyRatesForm()
    {
        InitializeComponent();
    }

    private void HourlyRatesForm_Load(object? sender, EventArgs e)
    {
        var types = BilliardRepository.GetTableTypes();
        var snooker = types.FirstOrDefault(t => t.TableTypeId == 1) ?? types.FirstOrDefault();
        var black = types.FirstOrDefault(t => t.TableTypeId == 2) ?? types.LastOrDefault();

        if (snooker != null)
        {
            _snookerTypeId = snooker.TableTypeId;
            numSnookerFirst.Value = snooker.FirstHourRate;
            numSnookerAdditional.Value = snooker.AdditionalHourRate;
        }
        if (black != null)
        {
            _blackTypeId = black.TableTypeId;
            numBlackFirst.Value = black.FirstHourRate;
            numBlackAdditional.Value = black.AdditionalHourRate;
        }
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        try
        {
            BilliardRepository.UpdatePricingRates(_snookerTypeId, numSnookerFirst.Value, numSnookerAdditional.Value);
            BilliardRepository.UpdatePricingRates(_blackTypeId, numBlackFirst.Value, numBlackAdditional.Value);
            MessageBox.Show("تم حفظ الأسعار.", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
